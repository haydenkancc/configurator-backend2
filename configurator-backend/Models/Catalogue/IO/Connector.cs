using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.IO
{
    public class ConnectorListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ConnectorListItem(Connector connector)
        {
            ID = connector.ID;
            Name = connector.Name;
        }
    }

    public class ConnectorParams
    {
        public required ICollection<Connector> Connectors { get; set; }
    }

    public class ConnectorDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Connector> CompatibleConnectors { get; set; }

        public ConnectorDto(Connector connector)
        {
            ID = connector.ID;
            Name = connector.Name;
            CompatibleConnectors = [.. connector.CompatibleConnectors];
        }
    }

    public class ConnectorDbo
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required List<int> CompatibleConnectorIDs { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Connector
    {

        public int ID { get; set; }
        public required string Name { get; set; }
        
        public ICollection<Connector> CompatibleConnectors { get; set; } = new List<Connector>();


        [JsonIgnore]
        public ICollection<Connector> PhysicalConnectors { get; set; } = new List<Connector>();


        [JsonIgnore]
        public ICollection<Case.UnitIOConnector> Cases { get; set; } = new List<Case.UnitIOConnector>();
        [JsonIgnore]
        public ICollection<Cooler.UnitConnector> Coolers { get; set; } = new List<Cooler.UnitConnector>();
        [JsonIgnore]
        public ICollection<Fan.PackConnector> Fans { get; set; } = new List<Fan.PackConnector>();
        [JsonIgnore]
        public ICollection<Motherboard.UnitIOConnector> Motherboards { get; set; } = new List<Motherboard.UnitIOConnector>();
        [JsonIgnore]
        public ICollection<Storage.CaseUnit> StorageUnits { get; set; } = new List<Storage.CaseUnit>();
    }
}
