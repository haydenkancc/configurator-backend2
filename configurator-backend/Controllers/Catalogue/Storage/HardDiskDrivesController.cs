using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models.Catalogue.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    [Route("api/Storage/[controller]")]
    [ApiController]
    public class HardDiskDrivesController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;

        public HardDiskDrivesController(CatalogueContext context, ComponentsController componentsController)
        {
            _context = context;
            _componentsController = componentsController;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<DriveDto>> GetHardDiskDrive(int id)
        {
            var d = await _componentsController.GetComponent(id);
            var component = d.Value;

            if (component is null)
            {
                return d.Result ?? BadRequest(ModelState);
            }

            var drive = await _context.StorageHardDiskDrives
                .AsNoTracking()
                .Where(e => id == e.UnitID)
                .FirstOrDefaultAsync();

            if (drive is null)
            {
                return NotFound();
            }

            return new DriveDto(Models.Catalogue.Storage.Type.HardDiskDrive, new HardDiskDriveDto(drive));
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> PutHardDiskDrive(int id, HardDiskDriveDbo drive)
        {
            var driveToUpdate = await _context.StorageHardDiskDrives.FirstOrDefaultAsync(drive => id == drive.UnitID);

            if (driveToUpdate == null)
            {
                return NotFound();
            }

            if (!HardDiskDriveIsValid(drive))
            {
                return BadRequest(ModelState);
            }

            driveToUpdate.Rpm = drive.Rpm;

            _context.Entry(driveToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HardDiskDriveExists(id))
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
        public ActionResult<Drive> PostHardDiskDrive(HardDiskDriveDbo drive)
        {
            if (!HardDiskDriveIsValid(drive))
            {
                return BadRequest(ModelState);
            }

            var emptyHardDiskDrive = new HardDiskDrive
            {
                Rpm = drive.Rpm,
            };

            _context.StorageHardDiskDrives.Add(emptyHardDiskDrive);

            return emptyHardDiskDrive;
        }

        private bool HardDiskDriveExists(int id)
        {
            return _context.StorageHardDiskDrives.Any(e => e.UnitID == id);
        }

        private bool HardDiskDriveIsValid(HardDiskDriveDbo drive)
        {

            if (drive.Rpm <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
