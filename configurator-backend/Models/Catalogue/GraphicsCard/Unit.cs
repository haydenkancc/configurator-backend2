using configurator_backend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.GraphicsCard
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
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

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

    public class UnitDbo : IPCIeExpansionCardDbo
    {
        public PCIeExpansionCardDbo ExpansionCard { get; set; }
        public int ChipsetID { get; set; }
        public int MemoryCapacityID { get; set; }
        public int MemoryTypeID { get; set; }

        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int TotalSlotWidth { get; set; }
        public int TotalPower { get; set; }
        public int RecommendedPower { get; set; }
        public int CoreClock { get; set; }
        public int BoostClock { get; set; }
        public ICollection<UnitConfigurationDbo> Configurations { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public int ComponentID { get; set; }
        public int ExpansionCardID { get; set; }
        public int ChipsetID { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int TotalSlotWidth { get; set; }
        public int TotalPower { get; set; }
        public int RecommendedPower { get; set; }

        public int MemoryCapacity { get; set; }
        public int MemoryTypeID { get; set; }
        public int CoreClock { get; set; }
        public int BoostClock { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public required Component Component { get; set; }
        public required Pcie.ExpansionCard ExpansionCard { get; set; }
        public required Chipset Chipset { get; set; }
        public required Memory.Type MemoryType { get; set; }
        public required ICollection<Configuration> Configurations { get; set; }
    }
}
