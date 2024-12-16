using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.GraphicsCard
{
    public class ChipsetListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ChipsetListItem(Chipset chipset)
        {
            ID = chipset.ID;
            Name = chipset.Name;
        }
    }

    public class ChipsetDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ChipsetDto(Chipset chipset)
        {
            ID = chipset.ID;
            Name = chipset.Name;
        }
    }

    public class ChipsetDbo
    {
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Chipset
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit>? Units { get; set; }
}
