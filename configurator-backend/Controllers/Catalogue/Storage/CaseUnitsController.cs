using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models.Catalogue.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class CaseUnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;
        private readonly DrivesController _drivesController;

        public CaseUnitsController(CatalogueContext context, ComponentsController componentsController, DrivesController drivesController)
        {
            _context = context;
            _componentsController = componentsController;
            _drivesController = drivesController;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UnitDto>> GetCaseUnit(int id)
        {
            var c = await _componentsController.GetComponent(id);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }

            var d = await _drivesController.GetDrive(id);
            var drive = d.Value;

            if (drive is null)
            {
                return d.Result ?? BadRequest(ModelState);
            }

            var unit = await _context.StorageCaseUnits
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.ConnectionInterface)
                .Include(unit => unit.PowerSupplyConnector)
                .Include(unit => unit.IOConnector)
                .Include(unit => unit.FormFactor)
                .FirstOrDefaultAsync();

            if (unit is null)
            {
                return NotFound();
            }

            return new UnitDto(Location.Case, new CaseUnitDto(component, drive, unit));
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutCaseUnit(int id, CaseUnitDbo unit)
        {

            var componentResult = await _componentsController.PutComponent(id, unit.Component);

            if (componentResult is not NoContentResult)
            {
                return componentResult;
            }

            var driveResult = await _drivesController.PutDrive(id, unit.Drive);

            if (driveResult is not NoContentResult)
            {
                return driveResult;
            }

            var unitToUpdate = await _context.StorageCaseUnits.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await CaseUnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            unitToUpdate.FormFactorID = unit.FormFactorID;
            unitToUpdate.IOConnectorID = unit.IOConnectorID;
            unitToUpdate.PowerSupplyConnectorID = unit.PowerSupplyConnectorID;

            unitToUpdate.ConnectionInterfaceID = unit.ConnectionInterfaceID;
            unitToUpdate.Capacity = unit.Capacity;
            unitToUpdate.Cache = unit.Cache;
            unitToUpdate.ReadSpeed = unit.ReadSpeed;
            unitToUpdate.WriteSpeed = unit.WriteSpeed;

            _context.Entry(unitToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseUnitExists(id))
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
        public async Task<ActionResult<Unit>> PostCaseUnit(CaseUnitDbo unit)
        {
            var c = await _componentsController.PostComponent(unit.Component);
            var component = c.Value;

            if (component is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }

            var d = await _drivesController.PostDrive(unit.Drive);
            var drive = d.Value;

            if (drive is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }


            if (!await CaseUnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            var emptyUnit = new CaseUnit
            {
                ComponentID = component.ID,
                FormFactorID = unit.FormFactorID,
                IOConnectorID = unit.IOConnectorID,
                PowerSupplyConnectorID = unit.PowerSupplyConnectorID,
                Drive = drive,
                ConnectionInterfaceID = unit.ConnectionInterfaceID,
                Capacity = unit.Capacity,
                Cache = unit.Cache,
                ReadSpeed = unit.ReadSpeed,
                WriteSpeed = unit.WriteSpeed
            };


            _context.StorageCaseUnits.Add(emptyUnit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCaseUnit), new { id = emptyUnit.ComponentID }, emptyUnit);
        }

        private bool CaseUnitExists(int id)
        {
            return _context.StorageCaseUnits.Any(e => e.ComponentID == id);
        }

        private async Task<bool> CaseUnitIsValid(CaseUnitDbo unit)
        {

            if (unit.Capacity <= 0 || unit.Cache < 0 || unit.ReadSpeed <= 0 || unit.WriteSpeed <= 0)
            {
                return false;
            }

            if (!await _context.StorageConnectionInterfaces.AnyAsync(e => unit.ConnectionInterfaceID == e.ID) ||
                !await _context.StorageFormFactors.AnyAsync(e => unit.FormFactorID == e.ID) ||
                !await _context.IOConnectors.AnyAsync(e => unit.IOConnectorID == e.ID) ||
                !await _context.PowerSupplyConnectors.AnyAsync(e => unit.PowerSupplyConnectorID == e.ID)
               )
            {
                return false;
            }

            return true;
        }
    }
}

