using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class ExpansionSlotAreaDto
    {
        public Pcie.BracketDto Bracket { get; set; }
        public int SlotCount { get; set; }
        public bool RiserRequired { get; set; }
        
        public ExpansionSlotAreaDto(ExpansionSlotArea area)
        {
            Bracket = new Pcie.BracketDto(area.Bracket);
            SlotCount = area.SlotCount;
            RiserRequired = area.RiserRequired;
        }
    }
    public class ExpansionSlotAreaDbo
    {
        [Required]
        public required int BracketID { get; set; }
        [Required]
        public required int SlotCount { get; set; }
        [Required]
        public required bool RiserRequired { get; set; }
    }

    [PrimaryKey(nameof(UnitID))]
    public class ExpansionSlotArea
    {
        public int UnitID { get; set; }
        public required int BracketID { get; set; }
        public required int SlotCount { get; set; }
        public required bool RiserRequired { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UnitID))]
        public Unit Unit { get; set; } = null!;
        public Pcie.Bracket Bracket { get; set; } = null!;
    }
}
