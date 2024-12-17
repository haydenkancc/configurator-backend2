using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Motherboard
{
    public class UnitListItem : ComponentListItem
    {
        public string Socket { get; set; }
        public string FormFactor { get; set; }
        public string MemoryTotalCapacity { get; set; }
        public int MemorySlotCount { get; set; }
        public UnitListItem(Unit motherboard) : base(motherboard.Component)
        {
            Socket = motherboard.Chipset.Socket.Name;
            FormFactor = motherboard.FormFactor.Name;
            MemoryTotalCapacity = $"{motherboard.MemoryTotalCapacity} GB";
            MemorySlotCount = motherboard.MemorySlotCount;
        }
    }

    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<IO.Connector> IOConnectors { get; set; }
        public required ICollection<M2.SlotListItemSimple> M2Slots { get; set; }
        public required ICollection<Pcie.SlotListItemSimple> PcieSlots { get; set; }
        public required ICollection<PowerSupply.Connector> PowerSupplyConnectors { get; set; }
        public required ICollection<Chipset> Chipsets { get; set; }
        public required ICollection<FormFactor> FormFactors { get; set; }
        public required ICollection<CentralProcessor.Channel> Channels { get; set; }
        public required ICollection<CentralProcessor.Series> Series { get; set; }
        public required ICollection<CentralProcessor.UnitListItemSimple> Processors { get; set; }
        public required ICollection<CentralProcessor.CoreFamily> CoreFamilies { get; set; }
        public required ICollection<Memory.FormFactor> MemoryFormFactors { get; set; }
        public required ICollection<Memory.Type> MemoryTypes { get; set; }
    }

    public class UnitDto
    {
        public ComponentDto Component { get; set; }
        public ICollection<UnitIOConnector> IOConnectors { get; set; }
        public ICollection<UnitM2Slot> M2Slots { get; set; }
        public ICollection<UnitPcieSlot> PcieSlots { get; set; }
        public ICollection<UnitPowerSupplyConnector> PowerSupplyConnectors { get; set; }
        public Chipset Chipset { get; set; }
        public FormFactor FormFactor { get; set; }
        public CentralProcessor.Channel Channel { get; set; }
        public Memory.FormFactor MemoryFormFactor { get; set; }
        public Memory.Type MemoryType { get; set; }
        public int MemoryTotalCapacity { get; set; }
        public int MemorySlotCount { get; set; }
        public bool SupportECCMemory { get; set; }
        public bool SupportNonECCMemory { get; set; }
        public bool SupportBufferedMemory { get; set; }
        public bool SupportUnbufferedMemory { get; set; }

        public UnitDto(ComponentDto component, Unit unit)
        {
            Component = component;

            IOConnectors = unit.IOConnectors;
            M2Slots = unit.M2Slots;
            PcieSlots = unit.PcieSlots;
            PowerSupplyConnectors = unit.PowerSupplyConnectors;

            Chipset = unit.Chipset;
            FormFactor = unit.FormFactor;
            Channel = unit.Channel;
            MemoryFormFactor = unit.MemoryFormFactor;
            MemoryType = unit.MemoryType;
            MemoryTotalCapacity = unit.MemoryTotalCapacity;
            MemorySlotCount = unit.MemorySlotCount;
            SupportECCMemory = unit.SupportECCMemory;
            SupportNonECCMemory = unit.SupportNonECCMemory;
            SupportBufferedMemory = unit.SupportBufferedMemory;
            SupportUnbufferedMemory = unit.SupportUnbufferedMemory;
        }
    }

    public class UnitDbo
    {
        [Required]
        public required ComponentDbo Component { get; set; }
        [Required]
        public required ICollection<UnitIOConnectorDbo> IOConnectors { get; set; }
        [Required]
        public required ICollection<UnitM2SlotDbo> M2Slots { get; set; }
        [Required]
        public required ICollection<UnitPcieSlotDbo> PcieSlots { get; set; }
        [Required]
        public required ICollection<UnitPowerSupplyConnectorDbo> PowerSupplyConnectors { get; set; }
        [Required]
        public required int ChipsetID { get; set; }
        [Required]
        public required int FormFactorID { get; set; }
        [Required]
        public required int ChannelID { get; set; }
        [Required]
        public required int MemoryFormFactorID { get; set; }
        [Required]
        public required int MemoryTypeID { get; set; }
        [Required]
        public required int MemoryTotalCapacityID { get; set; }
        [Required]
        public required int MemorySlotCount { get; set; }
        [Required]
        public required bool SupportECCMemory { get; set; }
        [Required]
        public required bool SupportNonECCMemory { get; set; }
        [Required]
        public required bool SupportBufferedMemory { get; set; }
        [Required]
        public required bool SupportUnbufferedMemory { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public int ComponentID { get; set; }
        public required int ChipsetID { get; set; }
        public required int FormFactorID { get; set; }
        public required int ChannelID { get; set; }
        public required int MemoryFormFactorID { get; set; }
        public required int MemoryTypeID { get; set; }

        public required int MemoryTotalCapacity { get; set; }
        public required int MemorySlotCount { get; set; }
        public required bool SupportECCMemory { get; set; }
        public required bool SupportNonECCMemory { get; set; }
        public required bool SupportBufferedMemory { get; set; }
        public required bool SupportUnbufferedMemory { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public Component Component { get; set; } = null!;
        public Chipset Chipset { get; set; } = null!;
        public FormFactor FormFactor { get; set; } = null!;
        public CentralProcessor.Channel Channel { get; set; } = null!;
        public Memory.FormFactor MemoryFormFactor { get; set; } = null!;
        public Memory.Type MemoryType { get; set; } = null!;

        public ICollection<UnitIOConnector> IOConnectors { get; set; } = new List<UnitIOConnector>();
        public ICollection<UnitM2Slot> M2Slots { get; set; } = new List<UnitM2Slot>();
        public ICollection<UnitPcieSlot> PcieSlots { get; set; } = new List<UnitPcieSlot>();
        public ICollection<UnitPowerSupplyConnector> PowerSupplyConnectors { get; set; } = new List<UnitPowerSupplyConnector>();
    }
}
