using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.PowerSupply
{
    public class ModularityListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ModularityListItem(Modularity modularity)
        {
            ID = modularity.ID;
            Name = modularity.Name;
        }
    }

    public class ModularityDto(Modularity modularity)
    {
        public int ID { get; set; } = modularity.ID;
        public string Name { get; set; } = modularity.Name;
    }

    public class ModularityDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Modularity
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
