using configurator_backend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace configurator_backend.Models.Catalogue.Memory
{
    public class KitListItem : ComponentListItem
    {
        public string Modules { get; set; }
        public string Speed { get; set; }
        public string CASLatency { get; set; }
        public string FirstWordLatency { get; set; }

        public KitListItem(Kit kit) : base(kit.Component)
        {
            Name = $"{kit.Component.Manufacturer.Name} {kit.Component.Name} {kit.ModuleCount * kit.Capacity} GB";
            Modules = $"{kit.ModuleCount} x {kit.Capacity}GB";
            Speed = $"{kit.Type.Name}-{kit.ClockFrequency}";
            CASLatency = kit.CASLatency.ToString("G0") + " ns";
            FirstWordLatency = kit.FirstWordLatency.ToString("G0") + " ns";
        }
    }

    public class KitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<FormFactor> FormFactors { get; set; }
        public required ICollection<Type> Types { get; set; }
    }

    public class KitDto
    {
        public ComponentDto Component { get; set; }
        public int Capacity { get; set; }
        public int ClockFrequency { get; set; }
        public decimal Height { get; set; }
        public bool IsECC { get; set; }
        public bool IsBuffered { get; set; }
        public int ModuleCount { get; set; }

        public int CASLatency { get; set; }
        public decimal FirstWordLatency { get; set; }
        public decimal Voltage { get; set; }
        public string Timing { get; set; }

        public FormFactor FormFactor { get; set; }
        public Type Type { get; set; }

        public KitDto(ComponentDto component, Kit kit)
        {
            Component = component;
            Capacity = kit.Capacity;
            ClockFrequency = kit.ClockFrequency;
            Height = kit.Height;
            IsECC = kit.IsECC;
            IsBuffered = kit.IsBuffered;
            ModuleCount = kit.ModuleCount;
            CASLatency = kit.CASLatency;
            FirstWordLatency = kit.FirstWordLatency;
            Voltage = kit.Voltage;
            Timing = kit.Timing;
            FormFactor = kit.FormFactor;
            Type = kit.Type;
        }

    }



    public class KitDbo
    {
        public required ComponentDbo Component { get; set; }
        public int FormFactorID { get; set; }
        public int TypeID { get; set; }
        public int CapacityID { get; set; }
        public int ClockFrequency { get; set; }
        public decimal Height { get; set; }
        public bool IsECC { get; set; }
        public bool IsBuffered { get; set; }
        public int ModuleCount { get; set; }

        public int CASLatency { get; set; }
        public decimal FirstWordLatency { get; set; }
        public decimal Voltage { get; set; }
        public required string Timing { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Kit
    {
        public int ComponentID { get; set; }
        public int FormFactorID { get; set; }
        public int TypeID { get; set; }
        public int Capacity { get; set; }
        public int ClockFrequency { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Height { get; set; }
        public bool IsECC { get; set; }
        public bool IsBuffered { get; set; }
        public int ModuleCount { get; set; }

        public int CASLatency { get; set; }
        [Column(TypeName = "decimal(6,3)")]
        public decimal FirstWordLatency { get; set; }
        [Column(TypeName = "decimal(6,3)")]
        public decimal Voltage { get; set; }
        public required string Timing { get; set; }


        [ForeignKey(nameof(ComponentID))]
        public required Component Component { get; set; }
        public required FormFactor FormFactor { get; set; }
        public required Type Type { get; set; }
    }
}
