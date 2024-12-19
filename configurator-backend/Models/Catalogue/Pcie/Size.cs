using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Pcie
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
        public int ID { get; set; }
        public int LaneCount { get; set; }

        public SizeDto(Size size)
        {
            ID = size.ID;
            LaneCount = size.LaneCount;
        }
    }

    public class SizeDbo
    {
        [Required]
        public required int LaneCount { get; set; }
    }

    public class Size
    {
        public int ID { get; set; }
        public required int LaneCount { get; set; }


        [JsonIgnore]
        [InverseProperty(nameof(Slot.PhysicalSize))]
        public ICollection<Slot> PhysicalMatchingPcieSlots { get; set; } = new List<Slot>();
        [JsonIgnore]
        public ICollection<Slot> LaneMatchingPcieSlots { get; set; } = new List<Slot>();

        [JsonIgnore]
        [InverseProperty(nameof(ExpansionCard.PhysicalSize))]
        public ICollection<ExpansionCard> PhysicalMatchingPcieExpansionCards { get; set; } = new List<ExpansionCard>();
        [JsonIgnore]
        public ICollection<ExpansionCard> LaneMatchingPcieExpansionCards { get; set; } = new List<ExpansionCard>();

        [JsonIgnore]
        public ICollection<M2.Slot> M2Slots { get; set; } = new List<M2.Slot>();
    }
}
