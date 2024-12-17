using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.CentralProcessor
{
    public class SocketListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SocketListItem(Socket socket)
        {
            ID = socket.ID;
            Name = socket.Name;
        }
    }

    public class SocketDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SocketDto(Socket socket)
        {
            ID = socket.ID;
            Name = socket.Name;
        }
    }

    public class SocketDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Socket
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
        [JsonIgnore]
        public ICollection<Cooler.Unit> Coolers { get; set; } = new List<Cooler.Unit>();
    }
}
