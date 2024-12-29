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
        public ICollection<ConfigurationDto> Configurations { get; set; }
        public ChipsetDto Chipset { get; set; }
        public Memory.TypeDto MemoryType { get; set; }
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
            Configurations = unit.Configurations.Select(e => new ConfigurationDto(e)).ToList();
            Chipset = new ChipsetDto(unit.Chipset);
            MemoryType = new Memory.TypeDto(unit.MemoryType);
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
        public required ICollection<ChipsetDto> Chipsets { get; set; }
        public required ICollection<Memory.TypeDto> MemoryTypes { get; set; }
        public required ICollection<PowerSupply.ConnectorDtoSimple> Connectors { get; set; }
    }

    public class UnitDbo
    {
        [Required]
        public required ComponentDbo Component { get; set; } 
        [Required]
        public required Pcie.ExpansionCardDbo ExpansionCard { get; set; }
        [Required]
        public required int ChipsetID { get; set; }
        [Required]
        public required int MemoryTypeID { get; set; }

        [Required]
        public required int MemoryCapacity { get; set; }
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
        public required Component Component { get; set; }
        public required Pcie.ExpansionCard ExpansionCard { get; set; }
        public Chipset Chipset { get; set; } = null!;
        public Memory.Type MemoryType { get; set; } = null!;
        public required ICollection<Configuration> Configurations { get; set; }
    }
}
