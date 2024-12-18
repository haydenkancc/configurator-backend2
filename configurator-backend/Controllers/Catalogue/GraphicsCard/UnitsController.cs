using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.GraphicsCard;
using ConfiguratorBackend.Models.Catalogue.M2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.GraphicsCard
{
    [Route("api/GraphicsCard/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;
        private readonly Pcie.ExpansionCardsController _expansionCardsController;

        public UnitsController(CatalogueContext context, ComponentsController componentsController, Pcie.ExpansionCardsController expansionCardsController)
        {
            _context = context;
            _componentsController = componentsController;
            _expansionCardsController = expansionCardsController;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<UnitListItem>>> GetUnits(int pageIndex = 1, int pageSize = 20)
        {
            return await PaginatedList<UnitListItem>.CreateAsync(
                _context.GraphicsCardUnits
                .AsNoTracking()
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Manufacturer)
                .Include(unit => unit.Component)
                .ThenInclude(component => component.Colour)
                .Include(unit => unit.Chipset)
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

            var e = await _expansionCardsController.GetExpansionCard(id);
            var expansionCard = e.Value;

            if (expansionCard is null)
            {
                return e.Result ?? BadRequest(ModelState);
            }

            var unit = await _context.GraphicsCardUnits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.Chipset)
                .Include(unit => unit.MemoryType)
                .Include(unit => unit.Configurations)
                .ThenInclude(configuration => configuration.Connectors)
                .FirstOrDefaultAsync();

            if (unit is null)
            {
                return NotFound();
            }

            return new UnitDto(component, expansionCard, unit);
        }

        [HttpGet("params")]
        public async Task<ActionResult<UnitParams>> GetUnitParams()
        {

            return new UnitParams
            {
                Component = await _componentsController.GetComponentParams(),
                ExpansionCard = await _expansionCardsController.GetExpansionCardParams(),
                Chipsets = await _context.GraphicsCardChipsets.AsNoTracking().Select(e => new ChipsetDto(e)).ToListAsync(),
                Connectors = await _context.PowerSupplyConnectors.AsNoTracking().Select(e => new Models.Catalogue.PowerSupply.ConnectorDtoSimple(e)).ToListAsync(),
                MemoryTypes = await _context.MemoryTypes.AsNoTracking().Select(e => new Models.Catalogue.Memory.TypeDto(e)).ToListAsync(),
            };
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutUnit(int id, UnitDbo unit)
        {

            var componentResult = await _componentsController.PutComponent(id, unit.Component);

            if (componentResult is not NoContentResult)
            {
                return componentResult;
            }

            var expansionCardResult = await _expansionCardsController.PutExpansionCard(id, unit.ExpansionCard);

            if (expansionCardResult is not NoContentResult)
            {
                return expansionCardResult;
            }

            var unitToUpdate = await _context.GraphicsCardUnits.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

            unitToUpdate.ChipsetID = unit.ChipsetID;
            unitToUpdate.MemoryCapacity = unit.MemoryCapacity;
            unitToUpdate.MemoryTypeID = unit.MemoryTypeID;
            unitToUpdate.Length = unit.Length;
            unitToUpdate.Width = unit.Width;
            unitToUpdate.Height = unit.Height;
            unitToUpdate.TotalSlotWidth = unit.TotalSlotWidth;
            unitToUpdate.TotalPower = unit.TotalPower;
            unitToUpdate.RecommendedPower = unit.RecommendedPower;
            unitToUpdate.CoreClock = unit.CoreClock;
            unitToUpdate.BoostClock = unit.BoostClock;

            _context.GraphicsCardUnits
                       .Entry(unitToUpdate)
                       .Collection(e => e.Configurations)
                       .Query()
                       .Include(e => e.Connectors)
                       .Load();


            unitToUpdate.Configurations = unit.Configurations
                .Select(configuration => new Configuration
                {
                    Connectors = configuration.Connectors.Select(connector => new ConfigurationConnector
                    {
                        ConnectorID = connector.ConnectorID,
                        ConnectorCount = connector.ConnectorCount,
                    })
                    .ToList(),
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

            var e = await _expansionCardsController.PostExpansionCard(unit.ExpansionCard);
            var expansionCard = e.Value;

            if (expansionCard is null)
            {
                return e.Result ?? BadRequest(ModelState);
            }


            if (!await UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            var emptyUnit = new Unit
            {
                ComponentID = component.ID,
                ExpansionCardID = expansionCard.ID,
                ChipsetID = unit.ChipsetID,
                MemoryCapacity = unit.MemoryCapacity,
                MemoryTypeID = unit.MemoryTypeID,
                Length = unit.Length,
                Width = unit.Width,
                Height = unit.Height,
                TotalSlotWidth = unit.TotalSlotWidth,
                TotalPower = unit.TotalPower,
                RecommendedPower = unit.RecommendedPower,
                CoreClock = unit.CoreClock,
                BoostClock = unit.BoostClock,
                Configurations = unit.Configurations
                .Select(configuration => new Configuration
                {
                    Connectors = configuration.Connectors.Select(connector => new ConfigurationConnector
                    {
                        ConnectorID = connector.ConnectorID,
                        ConnectorCount = connector.ConnectorCount,
                    })
                    .ToList(),
                })
                .ToList()
            };


            _context.GraphicsCardUnits.Add(emptyUnit);
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
            return _context.GraphicsCardUnits.Any(e => e.ComponentID == id);
        }



        private async Task<bool> UnitIsValid(UnitDbo unit)
        {
            if (unit.Length <= 0 || unit.Width <= 0 || unit.Height <= 0 ||
                unit.TotalSlotWidth <= 0 || unit.TotalPower <= 0 || unit.RecommendedPower <= 0 ||
                unit.CoreClock <= 0 || unit.BoostClock <= 0
                )
            {
                return false;
            }


            if (!await _context.GraphicsCardChipsets.AnyAsync(e => e.ID == unit.ChipsetID) ||
                !await _context.MemoryTypes.AnyAsync(e => e.ID == unit.MemoryTypeID)
                )
            {
                return false;
            }

            foreach (var configuration in unit.Configurations)
            {
                foreach (var connector in configuration.Connectors)
                {
                    if (!await _context.PowerSupplyConnectors.AnyAsync(e => connector.ConnectorID == e.ID) || connector.ConnectorCount <= 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
