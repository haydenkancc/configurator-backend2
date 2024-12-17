using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public class NandTypeListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public NandTypeListItem(NandType type)
        {
            ID = type.ID;
            Name = type.Name;
        }
    }

    public class NandTypeDto(NandType type)
    {
        public int ID { get; set; } = type.ID;
        public string Name { get; set; } = type.Name;
    }

    public class NandTypeDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class NandType
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<SolidStateDrive> Drives { get; set; } = new List<SolidStateDrive>();
    }
}
