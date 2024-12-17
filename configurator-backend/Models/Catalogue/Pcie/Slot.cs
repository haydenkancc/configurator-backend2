﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Pcie
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
        [Required]
        public required int LaneSizeID { get; set; }
        [Required]
        public required int PhysicalSizeID { get; set; }
        [Required]
        public required int VersionID { get; set; }
    }

    public class Slot
    {
        public int ID { get; set; }
        public required int LaneSizeID { get; set; }
        public required int PhysicalSizeID { get; set; }
        public required int VersionID { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Size PhysicalSize { get; set; } = null!;
        public Size LaneSize { get; set; } = null!;
        public Version Version { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Motherboard.UnitPcieSlot> Motherboards { get; set; } = new List<Motherboard.UnitPcieSlot>();
    }
}