using configurator_backend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Pcie
{
    public class ExpansionCardParams
    {
        public required ICollection<Bracket> Brackets { get; set; }
        public required ICollection<Version> Versions { get; set; }
        public required ICollection<Size> Sizes { get; set; }
    }

    public class ExpansionCardDto
    {
        public Bracket Bracket { get; set; }
        public Version Version { get; set; }
        public Size LaneSize { get; set; }
        public Size PhysicalSize { get; set; }
        public int ExpansionSlotWidth { get; set; }

        public ExpansionCardDto(ExpansionCard expansionCard)
        {
            Bracket = expansionCard.Bracket;
            Version = expansionCard.Version;
            LaneSize = expansionCard.LaneSize;
            PhysicalSize = expansionCard.PhysicalSize;
            ExpansionSlotWidth = expansionCard.ExpansionSlotWidth;
        }
    }

    public class ExpansionCardDbo
    {
        public int BracketID { get; set; }
        public int VersionID { get; set; }
        public int LaneSizeID { get; set; }
        public int PhysicalSizeID { get; set; }
        public int ExpansionSlotWidth { get; set; }
    }

    public class ExpansionCard
    {
        public int ID { get; set; }
        public int BracketID { get; set; }
        public int VersionID { get; set; }
        public int LaneSizeID { get; set; }
        public int PhysicalSizeID { get; set; }
        public int ExpansionSlotWidth { get; set; }


        public required Bracket Bracket { get; set; }
        public required Version Version { get; set; }
        public required Size LaneSize { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public required Size PhysicalSize { get; set; }

        [JsonIgnore]
        public GraphicsCard.Unit? GraphicsProcessor { get; set; }
    }
}
