using System.ComponentModel.DataAnnotations;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public class HardDiskDriveDto : BaseDriveDto
    {
        public int Rpm { get; set; }

        public HardDiskDriveDto(HardDiskDrive drive) : base(drive)
        {
            Rpm = drive.Rpm;
        }
    }

    public class HardDiskDriveDbo : BaseDriveDbo
    {
        [Required]
        public required int Rpm { get; set; }
    }

    public class HardDiskDrive : Drive
    {
        public required int Rpm { get; set; }
    }
}
