using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models.Catalogue.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class SolidStateDrivesController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;

        public SolidStateDrivesController(CatalogueContext context, ComponentsController componentsController)
        {
            _context = context;
            _componentsController = componentsController;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<DriveDto>> GetSolidStateDrive(int id)
        {
            var d = await _componentsController.GetComponent(id);
            var component = d.Value;

            if (component is null)
            {
                return d.Result ?? BadRequest(ModelState);
            }

            var drive = await _context.StorageSolidStateDrives
                .AsNoTracking()
                .Where(e => id == e.UnitID)
                .FirstOrDefaultAsync();

            if (drive is null)
            {
                return NotFound();
            }

            return new DriveDto(Models.Catalogue.Storage.Type.SolidStateDrive, new SolidStateDriveDto(drive));
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutSolidStateDrive(int id, SolidStateDriveDbo drive)
        {
            var driveToUpdate = await _context.StorageSolidStateDrives.FirstOrDefaultAsync(drive => id == drive.UnitID);

            if (driveToUpdate == null)
            {
                return NotFound();
            }

            if (!await SolidStateDriveIsValid(drive))
            {
                return BadRequest(ModelState);
            }

            driveToUpdate.NandTypeID = drive.NandTypeID;

            _context.Entry(driveToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolidStateDriveExists(id))
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
        public async Task<ActionResult<Drive>> PostSolidStateDrive(SolidStateDriveDbo drive)
        {
            if (!await SolidStateDriveIsValid(drive))
            {
                return BadRequest(ModelState);
            }

            var emptySolidStateDrive = new SolidStateDrive
            {
                NandTypeID = drive.NandTypeID,
            };

            _context.StorageSolidStateDrives.Add(emptySolidStateDrive);

            return emptySolidStateDrive;
        }

        private bool SolidStateDriveExists(int id)
        {
            return _context.StorageSolidStateDrives.Any(e => e.UnitID == id);
        }

        private async Task<bool> SolidStateDriveIsValid(SolidStateDriveDbo drive)
        {

            if (!await _context.StorageNandTypes.AnyAsync(e => drive.NandTypeID == e.ID))
            {
                return false;
            }

            return true;
        }
    }
}
