using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.PowerSupply
{
    public class UnitConnectorDbo
    {
        public int ConnectorID { get; set; }
        public int ConnectorCount { get; set; }
        public int SplitCount { get; set; }
    }

    [PrimaryKey(nameof(UnitID), nameof(ConnectorID), nameof(SplitCount))]
    public class UnitConnector
    {
        public int UnitID { get; set; }
        public int ConnectorID { get; set; }
        public int ConnectorCount { get; set; }
        public int SplitCount { get; set; }

        [JsonIgnore]
        public required Unit Unit { get; set; }

        public required Connector Connector { get; set; }
    }
}
