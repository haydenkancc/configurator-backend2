using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.PowerSupply
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
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Modularity
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit>? Units { get; set; }
    }
}
