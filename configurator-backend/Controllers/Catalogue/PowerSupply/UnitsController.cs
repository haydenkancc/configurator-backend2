using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.PowerSupply;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.PowerSupply
{
    [Route("api/PowerSupply/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;

        public UnitsController(CatalogueContext context, ComponentsController componentsController)
        {
            _context = context;
            _componentsController = componentsController;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<UnitListItem>>> GetUnits(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<UnitListItem>.CreateAsync(
                _context.PowerSupplyUnits
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Colour)
                .Include(unit => unit.EfficiencyRating)
                .Include(unit => unit.FormFactor)
                .Include(unit => unit.Modularity)
                .Select(unit => new UnitListItem(unit)),
                pageIndex,
                pageSize
            );
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UnitDto>> GetUnit(int id)
        {
            var c = await _componentsController.GetComponent(id);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }

            var unit = await _context.PowerSupplyUnits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.EfficiencyRating)
                .Include(unit => unit.Modularity)
                .Include(unit => unit.Connectors)
                .ThenInclude(connectors => connectors.Connector)
                .Include(unit => unit.FormFactor)
                .FirstOrDefaultAsync();

            if (unit is null)
            {
                return NotFound();
            }

            return new UnitDto(component, unit);
        }

        [HttpGet("params")]
        public async Task<ActionResult<UnitParams>> GetUnitParams()
        {
            var unitParams = new UnitParams
            {
                Component = await _componentsController.GetComponentParams(),

                EfficiencyRatings = await _context.PowerSupplyEfficiencyRatings.AsNoTracking().Select(e => new EfficiencyRatingDto(e)).ToListAsync(),
                Connectors = await _context.PowerSupplyConnectors.AsNoTracking().Select(e => new ConnectorDtoSimple(e)).ToListAsync(),
                Modularities = await _context.PowerSupplyModularities.AsNoTracking().Select(e => new ModularityDto(e)).ToListAsync(),
                FormFactors = await _context.PowerSupplyFormFactors.AsNoTracking().Select(e => new FormFactorDto(e)).ToListAsync(),            
            };

            return unitParams;
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutUnit(int id, UnitDbo unit)
        {

            var componentResult = await _componentsController.PutComponent(id, unit.Component);

            if (componentResult is not NoContentResult)
            {
                return componentResult;
            }

            var unitToUpdate = await _context.PowerSupplyUnits.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

            unitToUpdate.FormFactorID = unit.FormFactorID;
            unitToUpdate.ModularityID = unit.ModularityID;
            unitToUpdate.EfficiencyRatingID = unit.EfficiencyRatingID;
            unitToUpdate.TotalPower = unit.TotalPower;
            unitToUpdate.Length = unit.Length;
            unitToUpdate.Fanless = unit.Fanless;

            _context.PowerSupplyUnits.Entry(unitToUpdate).Collection(e => e.Connectors).Load();
            unitToUpdate.Connectors = unit.Connectors
                .Select(e => new UnitConnector
                {
                    ConnectorID = e.ConnectorID,
                    ConnectorCount = e.ConnectorCount,
                    SplitCount = e.SplitCount,
                })
                .ToList();

            _context.Entry(unitToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> PostUnit(UnitDbo unit)
        {
            var c = await _componentsController.PostComponent(unit.Component);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }


            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            var emptyUnit = new Unit
            {
                Component = component,
                FormFactorID = unit.FormFactorID,
                ModularityID = unit.ModularityID,
                EfficiencyRatingID = unit.EfficiencyRatingID,
                TotalPower = unit.TotalPower,
                Length = unit.Length,
                Fanless = unit.Fanless,
                Connectors = unit.Connectors
                    .Select(e => new UnitConnector
                    {
                        ConnectorID = e.ConnectorID,
                        ConnectorCount = e.ConnectorCount,
                        SplitCount = e.SplitCount,
                    })
                    .ToList(),
            };


            _context.PowerSupplyUnits.Add(emptyUnit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUnit), new { id = emptyUnit.ComponentID }, emptyUnit);
        }


        [HttpDelete("id/{id}")]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            return await _componentsController.DeleteComponent(id);
        }

        private bool UnitExists(int id)
        {
            return _context.PowerSupplyUnits.Any(e => e.ComponentID == id);
        }



        private async Task<bool> UnitIsValid(UnitDbo unit)
        {
            if (unit.TotalPower <= 0 || unit.Length <= 0)
            {
                return false;
            }

            if (!await _context.PowerSupplyFormFactors.AnyAsync(e => e.ID == unit.FormFactorID) ||
                !await _context.PowerSupplyModularities.AnyAsync(e => e.ID == unit.ModularityID) ||
                !await _context.PowerSupplyEfficiencyRatings.AnyAsync(e => e.ID == unit.EfficiencyRatingID)
                )
            {
                return false;
            }

            foreach (var connector in unit.Connectors)
            {
                if (!await _context.PowerSupplyConnectors.AnyAsync(e => connector.ConnectorID == e.ID) ||
                    connector.SplitCount <= 0 ||
                    connector.ConnectorCount <= 0
                    )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
