using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public enum Type
    {
        SolidStateDrive = 0,
        HardDiskDrive = 1,
    }

    public class DriveDto
    {
        public Type Type { get; set; }
        public SolidStateDriveDto? SolidStateDrive { get; set; }
        public HardDiskDriveDto? HardDiskDrive { get; set; }
    }

    public abstract class BaseDriveDto
    {
        public BaseDriveDto(Drive drive)
        {

        }
    }

    public class DriveParams
    {
        public required ICollection<NandType> NandTypes { get; set; }
    }

    public class DriveDbo
    {
        [Required]
        public required Type Type { get; set; }
        [Required]
        public required SolidStateDriveDbo? SolidStateDrive { get; set; }
        [Required]
        public required HardDiskDriveDbo? HardDiskDrive { get; set; }
    }

    public abstract class BaseDriveDbo
    {

    }

    [PrimaryKey(nameof(UnitID))]
    public abstract class Drive
    {
        public int UnitID { get; set; }
        public Type Type { get; set; }

        [ForeignKey(nameof(UnitID))]
        public Unit Unit { get; set; } = null!;
    }
}
