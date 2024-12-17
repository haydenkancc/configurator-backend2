using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguratorBackend.Models.Catalogue.Memory
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
        [Required]
        public required ComponentDbo Component { get; set; }
        [Required]
        public required int FormFactorID { get; set; }
        [Required]
        public required int TypeID { get; set; }
        [Required]
        public required int CapacityID { get; set; }
        [Required]
        public required int ClockFrequency { get; set; }
        [Required]
        public required decimal Height { get; set; }
        [Required]
        public required bool IsECC { get; set; }
        [Required]
        public required bool IsBuffered { get; set; }
        [Required]
        public required int ModuleCount { get; set; }

        [Required]
        public required int CASLatency { get; set; }
        [Required]
        public required decimal FirstWordLatency { get; set; }
        [Required]
        public required decimal Voltage { get; set; }
        [Required]
        public required string Timing { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Kit
    {
        public int ComponentID { get; set; }
        public required int FormFactorID { get; set; }
        public required int TypeID { get; set; }
        public required int Capacity { get; set; }
        public required int ClockFrequency { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public required decimal Height { get; set; }
        public required bool IsECC { get; set; }
        public required bool IsBuffered { get; set; }
        public required int ModuleCount { get; set; }

        public required int CASLatency { get; set; }
        [Column(TypeName = "decimal(6,3)")]
        public required decimal FirstWordLatency { get; set; }
        [Column(TypeName = "decimal(6,3)")]
        public required decimal Voltage { get; set; }
        public required string Timing { get; set; }


        [ForeignKey(nameof(ComponentID))]
        public Component Component { get; set; } = null!;
        public FormFactor FormFactor { get; set; } = null!;
        public Type Type { get; set; } = null!;
    }
}
