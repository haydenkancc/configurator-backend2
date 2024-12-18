using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Pcie
{
    public class ExpansionCardParams
    {
        public required ICollection<BracketDto> Brackets { get; set; }
        public required ICollection<VersionDto> Versions { get; set; }
        public required ICollection<SizeDto> Sizes { get; set; }
    }

    public class ExpansionCardDto
    {
        public BracketDto Bracket { get; set; }
        public VersionDto Version { get; set; }
        public SizeDto LaneSize { get; set; }
        public SizeDto PhysicalSize { get; set; }
        public int ExpansionSlotWidth { get; set; }

        public ExpansionCardDto(ExpansionCard expansionCard)
        {
            Bracket = new BracketDto(expansionCard.Bracket);
            Version = new VersionDto(expansionCard.Version);
            LaneSize = new SizeDto(expansionCard.LaneSize);
            PhysicalSize = new SizeDto(expansionCard.PhysicalSize);
            ExpansionSlotWidth = expansionCard.ExpansionSlotWidth;
        }
    }

    public class ExpansionCardDbo
    {
        [Required]
        public required int BracketID { get; set; }
        [Required]
        public required int VersionID { get; set; }
        [Required]
        public required int LaneSizeID { get; set; }
        [Required]
        public required int PhysicalSizeID { get; set; }
        [Required]
        public required int ExpansionSlotWidth { get; set; }
    }

    public class ExpansionCard
    {
        public int ID { get; set; }
        public required int BracketID { get; set; }
        public required int VersionID { get; set; }
        public required int LaneSizeID { get; set; }
        public required int PhysicalSizeID { get; set; }
        public required int ExpansionSlotWidth { get; set; }


        public Bracket Bracket { get; set; } = null!;
        public Version Version { get; set; } = null!;
        public Size LaneSize { get; set; } = null!;
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Size PhysicalSize { get; set; } = null!;

        [JsonIgnore]
        public GraphicsCard.Unit? GraphicsProcessor { get; set; }
    }
}
