using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.PowerSupply
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
        public ICollection<UnitConnector> Units { get; set; } = new List<UnitConnector>();
        [JsonIgnore]
        public ICollection<Motherboard.UnitPowerSupplyConnector> Motherboards { get; set; } = new List<Motherboard.UnitPowerSupplyConnector>();
        [JsonIgnore]
        public ICollection<Storage.CaseUnit> StorageUnits { get; set; } = new List<Storage.CaseUnit>();
        [JsonIgnore]
        public ICollection<Case.UnitPowerSupplyConnector> Cases { get; set; } = new List<Case.UnitPowerSupplyConnector>();
        [JsonIgnore]
        public ICollection<GraphicsCard.ConfigurationConnector> GraphicsCards { get; set; } = new List<GraphicsCard.ConfigurationConnector>();
    }
}
