using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Pcie
{
    public class SizeListItem
    {
        public int ID { get; set; }
        public string LaneCount { get; set; }

        public SizeListItem(Size size)
        {
            ID = size.ID;
            LaneCount = $"x{size.LaneCount}";
        }
    }

    public class SizeDto
    {
        public int LaneCount { get; set; }

        public SizeDto(Size size)
        {
            LaneCount = size.LaneCount;
        }
    }

    public class SizeDbo
    {
        public int ID { get; set; }
        public int LaneCount { get; set; }
    }

    public class Size
    {
        public int ID { get; set; }
        public int LaneCount { get; set; }


        [JsonIgnore]
        [InverseProperty(nameof(Slot.PhysicalSize))]
        public ICollection<Slot>? PhysicalMatchingPcieSlots { get; set; }
        [JsonIgnore]
        public ICollection<Slot>? LaneMatchingPcieSlots { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(ExpansionCard.PhysicalSize))]
        public ICollection<ExpansionCard>? PhysicalMatchingPcieExpansionCards { get; set; }
        [JsonIgnore]
        public ICollection<ExpansionCard>? LaneMatchingPcieExpansionCards { get; set; }

        [JsonIgnore]
        public ICollection<M2.Slot>? M2Slots { get; set; }
    }
}
