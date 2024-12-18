using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models.Catalogue.Cooler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Cooler
{
    [Route("api/Cooler/[controller]")]
    [ApiController]
    public class AirUnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;

        public AirUnitsController(CatalogueContext context, ComponentsController componentsController)
        {
            _context = context;
            _componentsController = componentsController;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UnitDto>> GetAirUnit(int id)
        {
            var c = await _componentsController.GetComponent(id);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }

            var unit = await _context.CoolerAirUnits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.Sockets)
                .Include(unit => unit.Connectors)
                .FirstOrDefaultAsync();

            if (unit is null)
            {
                return NotFound();
            }

            return new UnitDto(Models.Catalogue.Cooler.Type.Air, new AirUnitDto(component, unit));
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutAirUnit(int id, AirUnitDbo unit)
        {

            var componentResult = await _componentsController.PutComponent(id, unit.Component);

            if (componentResult is not NoContentResult)
            {
                return componentResult;
            }

            var unitToUpdate = await _context.CoolerAirUnits.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await AirUnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            unitToUpdate.LimitsMemoryHeight = unit.LimitsMemoryHeight;
            unitToUpdate.MaximumMemoryHeight = unit.LimitsMemoryHeight ? unit.MaximumMemoryHeight : null;
            unitToUpdate.Height = unit.Height;
            unitToUpdate.IsPassive = unit.IsPassive;
            unitToUpdate.FanCount = !unit.IsPassive ? unit.FanCount : null;
            unitToUpdate.FanRpm = !unit.IsPassive ? unit.FanRpm : null;
            unitToUpdate.FanAirflow = !unit.IsPassive ? unit.FanAirflow : null;
            unitToUpdate.FanNoiseLevel = !unit.IsPassive ? unit.FanNoiseLevel : null;
            unitToUpdate.FanStaticPressure = !unit.IsPassive ? unit.FanStaticPressure : null;
            _context.CoolerAirUnits.Entry(unitToUpdate).Collection(e => e.Sockets).Load();
            unitToUpdate.Sockets = await _context.CentralProcessorSockets.Where(e => unit.SocketIDs.Contains(e.ID)).ToListAsync();

            _context.Entry(unitToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirUnitExists(id))
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
        public async Task<ActionResult<Unit>> PostAirUnit(AirUnitDbo unit)
        {
            var c = await _componentsController.PostComponent(unit.Component);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }


            if (!await AirUnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            var emptyAirUnit = new AirUnit
            {
                ComponentID = component.ID,
                LimitsMemoryHeight = unit.LimitsMemoryHeight,
                MaximumMemoryHeight = unit.LimitsMemoryHeight ? unit.MaximumMemoryHeight : null,
                Height = unit.Height,
                IsPassive = unit.IsPassive,
                FanCount = !unit.IsPassive ? unit.FanCount : null,
                FanRpm = !unit.IsPassive ? unit.FanRpm : null,
                FanAirflow = !unit.IsPassive ? unit.FanAirflow : null,
                FanNoiseLevel = !unit.IsPassive ? unit.FanNoiseLevel : null,
                FanStaticPressure = !unit.IsPassive ? unit.FanStaticPressure : null,
                Sockets = await _context.CentralProcessorSockets.Where(e => unit.SocketIDs.Contains(e.ID)).ToListAsync()
            };


            _context.CoolerAirUnits.Add(emptyAirUnit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAirUnit), new { id = emptyAirUnit.ComponentID }, emptyAirUnit);
        }

        private bool AirUnitExists(int id)
        {
            return _context.CoolerAirUnits.Any(e => e.ComponentID == id);
        }

        private async Task<bool> AirUnitIsValid(AirUnitDbo unit)
        {

            if (!unit.IsPassive &&
                (String.IsNullOrWhiteSpace(unit.FanRpm) ||
                 String.IsNullOrWhiteSpace(unit.FanAirflow) ||
                 String.IsNullOrWhiteSpace(unit.FanNoiseLevel) ||
                 String.IsNullOrWhiteSpace(unit.FanStaticPressure) ||
                 unit.FanCount is null ||
                 unit.FanCount <= 0
                )
               )
            {
                return false;
            }

            foreach (var connector in unit.Connectors)
            {
                if (!await _context.IOConnectors.AnyAsync(e => connector.ConnectorID == e.ID))
                {
                    return false;
                }
                if (connector.ConnectorCount <= 0)
                {
                    return false;
                }
            }

            foreach (var socketID in unit.SocketIDs)
            {
                if (!await _context.CentralProcessorSockets.AnyAsync(e => socketID == e.ID))
                {
                    return false;
                }
            }

            if (unit.Height <= 0)
            {
                return false;
            }

            if (unit.LimitsMemoryHeight && (unit.MaximumMemoryHeight is null || unit.MaximumMemoryHeight < 0))
            {
                return false;
            }

            return true;
        }
    }
}

