using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.CentralProcessor
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
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Socket
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit>? Units { get; set; }
    }
}
