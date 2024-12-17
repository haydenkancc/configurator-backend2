using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.General
{
    public class ColourListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ColourListItem(Colour colour)
        {
            ID = colour.ID;
            Name = colour.Name;
        }
    }

    public class ColourDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ColourDto(Colour colour)
        {
            ID = colour.ID;
            Name = colour.Name;
        }
    }

    public class ColourDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Colour
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
}
