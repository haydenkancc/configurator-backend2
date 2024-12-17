using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.PowerSupply
{
    public class EfficiencyRatingListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public EfficiencyRatingListItem(EfficiencyRating efficiencyRating)
        {
            ID = efficiencyRating.ID;
            Name = efficiencyRating.Name;
        }
    }

    public class EfficiencyRatingDto(EfficiencyRating efficiencyRating)
    {
        public int ID { get; set; } = efficiencyRating.ID;
        public string Name { get; set; } = efficiencyRating.Name;
    }

    public class EfficiencyRatingDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class EfficiencyRating
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
