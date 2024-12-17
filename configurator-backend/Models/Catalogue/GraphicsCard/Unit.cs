using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.GraphicsCard
{
    public class UnitListItem : ComponentListItem
    {
        public string Chipset { get; set; }
        public string MemoryCapacity { get; set; }
        public string CoreClock { get; set; }
        public string BoostClock { get; set; }
        public string Length { get; set; }

        public UnitListItem(Unit unit) : base(unit.Component)
        {
            Chipset = unit.Chipset.Name;
            MemoryCapacity = $"{unit.MemoryCapacity} GB";
            CoreClock = $"{unit.CoreClock} MHz";
            BoostClock = $"{unit.BoostClock} MHz";
            Length = $"{unit.Length} mm";
        }
    }

    public class UnitDto
    {
        public ComponentDto Component { get; set; }
        public Pcie.ExpansionCardDto ExpansionCard { get; set; }
        public ICollection<Configuration> Configurations { get; set; }
        public Chipset Chipset { get; set; }
        public Memory.Type MemoryType { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public int TotalSlotWidth { get; set; }
        public int TotalPower { get; set; }
        public int RecommendedPower { get; set; }
        public int MemoryCapacity { get; set; }
        public int CoreClock { get; set; }
        public int BoostClock { get; set; }

        public UnitDto(ComponentDto component, Pcie.ExpansionCardDto expansionCard, Unit unit)
        {
            Component = component;
            ExpansionCard = expansionCard;
            Configurations = unit.Configurations;
            Chipset = unit.Chipset;
            MemoryType = unit.MemoryType;
            Length = unit.Length;
            Width = unit.Width;
            Height = unit.Height;
            TotalSlotWidth = unit.TotalSlotWidth;
            TotalPower = unit.TotalPower;
            RecommendedPower = unit.RecommendedPower;
            MemoryCapacity = unit.MemoryCapacity;
            CoreClock = unit.CoreClock;
            BoostClock = unit.BoostClock;
        }
    }

    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required Pcie.ExpansionCardParams ExpansionCard { get; set; }
        public required ICollection<Chipset> Chipsets { get; set; }
        public required ICollection<Memory.Type> MemoryTypes { get; set; }
        public required ICollection<PowerSupply.Connector> Connectors { get; set; }
    }

    public class UnitDbo
    {
        [Required]
        public required Pcie.ExpansionCardDbo ExpansionCard { get; set; }
        [Required]
        public required int ChipsetID { get; set; }
        [Required]
        public required int MemoryCapacityID { get; set; }
        [Required]
        public required int MemoryTypeID { get; set; }

        [Required]
        public required int Length { get; set; }
        [Required]
        public required int Width { get; set; }
        [Required]
        public required int Height { get; set; }

        [Required]
        public required int TotalSlotWidth { get; set; }
        [Required]
        public required int TotalPower { get; set; }
        [Required]
        public required int RecommendedPower { get; set; }
        [Required]
        public required int CoreClock { get; set; }
        [Required]
        public required int BoostClock { get; set; }
        [Required]
        public required ICollection<ConfigurationDbo> Configurations { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public int ComponentID { get; set; }
        public int ExpansionCardID { get; set; }
        public required int ChipsetID { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Length { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Width { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Height { get; set; }

        public required int TotalSlotWidth { get; set; }
        public required int TotalPower { get; set; }
        public required int RecommendedPower { get; set; }
               
        public required int MemoryCapacity { get; set; }
        public required int MemoryTypeID { get; set; }
        public required int CoreClock { get; set; }
        public required int BoostClock { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public Component Component { get; set; } = null!;
        public Pcie.ExpansionCard ExpansionCard { get; set; } = null!;
        public Chipset Chipset { get; set; } = null!;
        public Memory.Type MemoryType { get; set; } = null!;
        public required ICollection<Configuration> Configurations { get; set; }
    }
}
