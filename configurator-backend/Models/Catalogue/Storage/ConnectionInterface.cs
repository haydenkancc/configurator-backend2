using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public class ConnectionInterfaceListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ConnectionInterfaceListItem(ConnectionInterface connectionInterface)
        {
            ID = connectionInterface.ID;
            Name = connectionInterface.Name;
        }
    }

    public class ConnectionInterfaceDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ConnectionInterfaceDto(ConnectionInterface connectionInterface)
        {
            ID = connectionInterface.ID;
            Name = connectionInterface.Name;
        }
    }

    public class ConnectionInterfaceDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class ConnectionInterface
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
