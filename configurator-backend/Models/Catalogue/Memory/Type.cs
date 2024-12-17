using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Memory
{
    public class TypeListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public TypeListItem(Type type)
        {
            ID = type.ID;
            Name = type.Name;
        }
    }

    public class TypeDto(Type type)
    {
        public int ID { get; set; } = type.ID;
        public string Name { get; set; } = type.Name;
    }

    public class TypeDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Type
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Kit> Kits { get; set; } = new List<Kit>();
        [JsonIgnore]
        public ICollection<Motherboard.Unit> Motherboards { get; set; } = new List<Motherboard.Unit>();
        [JsonIgnore]
        public ICollection<GraphicsCard.Unit> GraphicsCards { get; set; } = new List<GraphicsCard.Unit>();
    }
}
