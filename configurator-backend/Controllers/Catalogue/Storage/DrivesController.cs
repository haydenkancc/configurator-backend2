using Configurator.Data;
using ConfiguratorBackend.Controllers.Catalogue.General;
using ConfiguratorBackend.Models;
using ConfiguratorBackend.Models.Catalogue.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConfiguratorBackend.Controllers.Catalogue.Storage
{
    public class DrivesController : ControllerBase
    {
        private readonly CatalogueContext _context;
        private readonly ComponentsController _componentsController;
        private readonly SolidStateDrivesController _solidStateDrivesController;
        private readonly HardDiskDrivesController _hardDiskDrivesController;

        public DrivesController(CatalogueContext context, ComponentsController componentsController, SolidStateDrivesController solidStateDrivesController, HardDiskDrivesController hardDiskDrivesController)
        {
            _context = context;
            _componentsController = componentsController;
            _solidStateDrivesController = solidStateDrivesController;
            _hardDiskDrivesController = hardDiskDrivesController;
        }

        public async Task<ActionResult<DriveDto>> GetDrive(int id)
        {
            var drive = await _context.StorageDrives.FirstOrDefaultAsync(e => id == e.UnitID);

            if (drive is null)
            {
                return NotFound();
            }

            if (drive is HardDiskDrive)
            {
                return await _hardDiskDrivesController.GetHardDiskDrive(id);
            }
            else if (drive is SolidStateDrive)
            {
                return await _solidStateDrivesController.GetSolidStateDrive(id);
            }

            return BadRequest(ModelState);
        }

        public async Task<DriveParams> GetDriveParams()
        {
            var driveParams = new DriveParams
            {
                NandTypes = await _context.StorageNandTypes.AsNoTracking().Select(e => new NandTypeDto(e)).ToListAsync(),
            };

            return driveParams;
        }

        public async Task<IActionResult> PutDrive(int id, DriveDbo drive)
        {
            if (!DriveIsValid(drive))
            {
                return BadRequest(ModelState);
            }

            if (drive.Type is Models.Catalogue.Storage.Type.HardDiskDrive)
            {
                return await _hardDiskDrivesController.PutHardDiskDrive(id, drive.HardDiskDrive!);
            }
            else if (drive.Type is Models.Catalogue.Storage.Type.SolidStateDrive)
            {
                return await _solidStateDrivesController.PutSolidStateDrive(id, drive.SolidStateDrive!);
            }

            return BadRequest(ModelState);
        }

        public async Task<ActionResult<Drive>> PostDrive(DriveDbo drive)
        {
            if (!DriveIsValid(drive))
            {
                return BadRequest(ModelState);
            }

            if (drive.Type is Models.Catalogue.Storage.Type.HardDiskDrive)
            {
                return _hardDiskDrivesController.PostHardDiskDrive(drive.HardDiskDrive!);
            }
            else if (drive.Type is Models.Catalogue.Storage.Type.SolidStateDrive)
            {
                return await _solidStateDrivesController.PostSolidStateDrive(drive.SolidStateDrive!);
            }

            return BadRequest(ModelState);
        }

        public async Task<IActionResult> DeleteDrive(int id)
        {
            return await _componentsController.DeleteComponent(id);
        }

        private bool DriveIsValid(DriveDbo drive)
        {

            if (drive.Type is Models.Catalogue.Storage.Type.HardDiskDrive && drive.HardDiskDrive is null)
            {
                return false;
            }
            else if (drive.Type is Models.Catalogue.Storage.Type.SolidStateDrive && drive.SolidStateDrive is null)
            {
                return false;
            }

            return true;
        }
    }
}
