using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Motherboard
{
    public class ChipsetListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Socket { get; set; }

        public ChipsetListItem(Chipset chipset)
        {
            ID = chipset.ID;
            Name = chipset.Name;
            Socket = chipset.Socket.Name;
        }
    }

    public class ChipsetDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CentralProcessor.Socket Socket { get; set; }

        public ChipsetDto(Chipset chipset)
        {
            ID = chipset.ID;
            Name = chipset.Name;
            Socket = chipset.Socket;
        }
    }

    public class ChipsetDbo
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required int SocketID { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Chipset
    {
        public int ID { get; set; }
        public required int SocketID { get; set; }
        public required string Name { get; set; }

        public CentralProcessor.Socket Socket { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
