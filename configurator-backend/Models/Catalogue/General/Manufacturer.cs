using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.General
{
    public class ManufacturerListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ManufacturerListItem(Manufacturer manufacturer)
        {
            ID = manufacturer.ID;
            Name = manufacturer.Name;
        }
    }

    public class ManufacturerDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ManufacturerDto(Manufacturer manufacturer)
        {
            ID = manufacturer.ID;
            Name = manufacturer.Name;
        }
    }

    public class ManufacturerDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Manufacturer
    {
        public int ID { get; set; }
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}
