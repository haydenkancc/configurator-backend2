using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.IO
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
        public required string Name { get; set; }
        public List<int>? CompatibleConnectorIDs { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Connector
    {

        public int ID { get; set; }
        public required string Name { get; set; }
        
        public ICollection<Connector>? CompatibleConnectors { get; set; }


        [JsonIgnore]
        public ICollection<Connector>? PhysicalConnectors { get; set; }


        [JsonIgnore]
        public ICollection<CaseUnitFrontIOConnector>? CaseFrontIOs { get; set; }
        [JsonIgnore]
        public ICollection<CoolerUnitIOConnector>? Coolers { get; set; }
        [JsonIgnore]
        public ICollection<Fan.PackConnector>? Fans { get; set; }
        [JsonIgnore]
        public ICollection<MotherboardUnitIOConnector>? Motherboards { get; set; }
        [JsonIgnore]
        public ICollection<CaseStorageUnit>? StorageUnits { get; set; }
    }
}
