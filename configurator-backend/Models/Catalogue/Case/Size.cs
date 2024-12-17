using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class SizeListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SizeListItem(Size size)
        {
            ID = size.ID;
            Name = size.Name;
        }
    }

    public class SizeDto(Size size)
    {
        public int ID { get; set; } = size.ID;
        public string Name { get; set; } = size.Name;
    }

    public class SizeDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Size
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit> Cases { get; set; } = new List<Unit>();
    }
}
