using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Pcie
{
    public class BracketListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public BracketListItem(Bracket bracket)
        {
            ID = bracket.ID;
            Name = bracket.Name;
        }
    }

    public class BracketDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public BracketDto(Bracket bracket)
        {
            ID = bracket.ID;
            Name = bracket.Name;
        }
    }

    public class BracketDbo
    {
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Bracket
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<ExpansionCard>? ExpansionCards { get; set; }

        [JsonIgnore]
        public ICollection<CaseUnitExpansionSlotArea>? ExpansionSlotAreas { get; set; }
    }
}
