using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Pcie
{
    public class SlotListItemSimple
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SlotListItemSimple(Slot slot)
        {
            ID = slot.ID;
            Name = $"{slot.Version.Name} x{slot.PhysicalSize.LaneCount}{(slot.PhysicalSize.LaneCount != slot.LaneSize.LaneCount ? $" @x{slot.LaneSize.LaneCount}" : "")}";
        }
    }

    public class SlotParams
    {
        public required ICollection<Size> Sizes { get; set; }
        public required ICollection<Version> Versions { get; set; }
    }

    public class SlotListItem
    {
        public int ID { get; set; }
        public string SlotSize { get; set; }
        public string Version { get; set; }

        public SlotListItem(Slot slot)
        {
            ID = slot.ID;
            SlotSize = $"x{slot.PhysicalSize.LaneCount}{(slot.PhysicalSize.LaneCount != slot.LaneSize.LaneCount ? $" @x{slot.LaneSize.LaneCount}" : "")}";
            Version = slot.Version.Name;
        }
    }

    public class SlotDto
    {
        public int ID { get; set; }
        public Size LaneSize { get; set; }
        public Size PhysicalSize { get; set; }
        public Version Version { get; set; }

        public SlotDto(Slot pcieSlot)
        {
            ID = pcieSlot.ID;
            LaneSize = pcieSlot.LaneSize;
            PhysicalSize = pcieSlot.PhysicalSize;
            Version = pcieSlot.Version;
        }
    }

    public class SlotDbo
    {
        public int LaneSizeID { get; set; }
        public int PhysicalSizeID { get; set; }
        public int VersionID { get; set; }
    }

    public class Slot
    {
        public int ID { get; set; }
        public int LaneSizeID { get; set; }
        public int PhysicalSizeID { get; set; }
        public int VersionID { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public required Size PhysicalSize { get; set; }
        public required Size LaneSize { get; set; }
        public required Version Version { get; set; }

        [JsonIgnore]
        public ICollection<MotherboardUnitSlot>? Motherboards { get; set; }
    }
}
