using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class MaterialListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MaterialListItem(Material material)
        {
            ID = material.ID;
            Name = material.Name;
        }
    }

    public class MaterialDto(Material material)
    {
        public int ID { get; set; } = material.ID;
        public string Name { get; set; } = material.Name;
    }

    public class MaterialDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Material
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit> Cases { get; set; } = new List<Unit>();
    }
}
