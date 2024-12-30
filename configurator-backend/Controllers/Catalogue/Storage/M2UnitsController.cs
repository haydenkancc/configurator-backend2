using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models.Catalogue.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class M2UnitsController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;
        private readonly M2.ExpansionCardsController _expansionCardsController;
        private readonly DrivesController _drivesController;

        public M2UnitsController(CatalogueContext context, ComponentsController componentsController, M2.ExpansionCardsController expansionCardsController, DrivesController drivesController)
        {
            _context = context;
            _componentsController = componentsController;
            _expansionCardsController = expansionCardsController;
            _drivesController = drivesController;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UnitDto>> GetM2Unit(int id)
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

            var d = await _drivesController.GetDrive(id);
            var drive = d.Value;

            if (drive is null)
            {
                return d.Result ?? BadRequest(ModelState);
            }

            var unit = await _context.StorageM2Units
                .AsNoTracking()
                .Where(e => id == e.ComponentID)
                .Include(unit => unit.ConnectionInterface)
                .FirstOrDefaultAsync();

            if (unit is null)
            {
                return NotFound();
            }

            return new UnitDto(Location.M2, new M2UnitDto(component, drive, unit, expansionCard));
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutM2Unit(int id, M2UnitDbo unit)
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

            var driveResult = await _drivesController.PutDrive(id, unit.Drive);

            if (driveResult is not NoContentResult)
            {
                return driveResult;
            }

            var unitToUpdate = await _context.StorageM2Units.FirstOrDefaultAsync(unit => id == unit.ComponentID);

            if (unitToUpdate == null)
            {
                return NotFound();
            }

            if (!await M2UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }

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
                if (!M2UnitExists(id))
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
        public async Task<ActionResult<Unit>> PostM2Unit(M2UnitDbo unit)
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
                return c.Result ?? BadRequest(ModelState);
            }

            var d = await _drivesController.PostDrive(unit.Drive);
            var drive = d.Value;

            if (drive is null)
            {
                return c.Result ?? BadRequest(ModelState);
            }


            if (!await M2UnitIsValid(unit))
            {
                return BadRequest(ModelState);
            }


            var emptyUnit = new M2Unit
            {
                Component = component,
                ExpansionCard = expansionCard,
                Drive = drive,
                ConnectionInterfaceID = unit.ConnectionInterfaceID,
                Capacity = unit.Capacity,
                Cache = unit.Cache,
                ReadSpeed = unit.ReadSpeed,
                WriteSpeed = unit.WriteSpeed
            };


            _context.StorageM2Units.Add(emptyUnit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetM2Unit), new { id = emptyUnit.ComponentID }, emptyUnit);
        }

        private bool M2UnitExists(int id)
        {
            return _context.StorageM2Units.Any(e => e.ComponentID == id);
        }

        private async Task<bool> M2UnitIsValid(M2UnitDbo unit)
        {

            if (unit.Capacity <= 0 || unit.Cache < 0 || unit.ReadSpeed <= 0 || unit.WriteSpeed <= 0)
            {
                return false;
            }

            if (!await _context.StorageConnectionInterfaces.AnyAsync(e => unit.ConnectionInterfaceID == e.ID))
            {
                return false;
            }

            return true;
        }
    }
}

