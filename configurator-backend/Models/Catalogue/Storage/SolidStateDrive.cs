using System.ComponentModel.DataAnnotations;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public class SolidStateDriveDto : BaseDriveDto
    {
        public NandTypeDto NandType { get; set; }

        public SolidStateDriveDto(SolidStateDrive drive) : base(drive)
        {
            NandType = new NandTypeDto(drive.NandType);
        }

    }

    public class SolidStateDriveDbo : BaseDriveDbo
    {
        [Required]
        public required int NandTypeID { get; set; }
    }

    public class SolidStateDrive : Drive
    {
        public required int NandTypeID { get; set; }

        public NandType NandType { get; set; } = null!;
    }
}
