using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Fan
{
    public class PackConnectorDbo
    {
        public int ConnectorID { get; set; }
        public int ConnectorCount { get; set; }
    }

    [PrimaryKey(nameof(PackID), nameof(ConnectorID))]
    public class PackConnector
    {

        public int PackID { get; set; }
        public int ConnectorID { get; set; }
        public int ConnectorCount { get; set; }


        [DeleteBehavior(DeleteBehavior.Cascade)]
        [JsonIgnore]
        public required Pack Pack { get; set; }
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public required IO.Connector Connector { get; set; }
    }
}
