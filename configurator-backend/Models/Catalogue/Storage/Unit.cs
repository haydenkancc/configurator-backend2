using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public enum Location
    {
        Case = 0,
        M2 = 1,
    }

    public class UnitListItem : ComponentListItem
    {
        public string Capacity { get; set; }
        public string Cache { get; set; }
        public string FormFactor { get; set; }
        public string ConnectionInterface { get; set; }
        public string Type { get; set; }

        public UnitListItem(Unit unit) : base(unit.Component)
        {
            Capacity = unit.Capacity >= 1000 ? $"{Decimal.Divide(unit.Capacity, 1000m)} TB" : $"{unit.Capacity} GB";
            Cache = unit.Cache >= 1000 ? $"{Decimal.Divide(unit.Cache, 1000m)} GB" : $"{unit.Cache} MB";
            FormFactor = "N/A";
            ConnectionInterface = unit.ConnectionInterface.Name;
            Type = unit.Drive.Type is Storage.Type.HardDiskDrive ? "HDD" : "SSD";
        }
    }

    public class UnitDto
    {
        public Location Location { get; set; }
        public CaseUnitDto? CaseUnit { get; set; }
        public M2UnitDto? M2Unit { get; set; }
    }

    public abstract class BaseUnitDto
    {
        public ComponentDto Component { get; set; }
        public DriveDto Drive { get; set; }

        public ConnectionInterface ConnectionInterface { get; set; }
        public int Capacity { get; set; }
        public int Cache { get; set; }
        public int ReadSpeed { get; set; }
        public int WriteSpeed { get; set; }

        public BaseUnitDto(ComponentDto component, DriveDto drive, Unit unit)
        {
            Component = component;
            Drive = drive;
            ConnectionInterface = unit.ConnectionInterface;
            Capacity = unit.Capacity;
            Cache = unit.Cache;
            ReadSpeed = unit.ReadSpeed;
            WriteSpeed = unit.WriteSpeed;
        }
    }

    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required M2.ExpansionCardParams ExpansionCard { get; set; }
        public required DriveParams Drive { get; set; }
        public required ICollection<ConnectionInterface> ConnectionInterfaces { get; set; }
    }

    public class UnitDbo
    {
        [Required]
        public required Location Location { get; set; }
        [Required]
        public required CaseUnitDbo? CaseUnit { get; set; }
        [Required]
        public required M2UnitDbo? M2Unit { get; set; }
    }

    public abstract class BaseUnitDbo
    {
        [Required]
        public required ComponentDbo Component { get; set; }
        [Required]
        public required DriveDbo Drive { get; set; }
        [Required]
        public required int ConnectionInterfaceID { get; set; }
        [Required]
        public required int Capacity { get; set; }
        [Required]
        public required int Cache { get; set; }
        [Required]
        public required int ReadSpeed { get; set; }
        [Required]
        public required int WriteSpeed { get; set; }
    }


    [PrimaryKey(nameof(ComponentID))]
    public abstract class Unit
    {
        public int ComponentID { get; set; }
        public required int ConnectionInterfaceID { get; set; }
        public Location Location { get; set; }

        public required int Capacity { get; set; }
        public required int Cache { get; set; }
        public required int ReadSpeed { get; set; }
        public required int WriteSpeed { get; set; }
        public required ConnectionInterface ConnectionInterface { get; set; }
        public required Drive Drive { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public Component Component { get; set; } = null!;
    }
}
