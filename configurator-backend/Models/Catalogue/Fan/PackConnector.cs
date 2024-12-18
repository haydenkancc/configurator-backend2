using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Fan
{
    public class PackConnectorDto
    {
        public IO.ConnectorDtoSimple Connector { get; set; }
        public int ConnectorCount { get; set; }

        public PackConnectorDto(PackConnector packConnector)
        {
            Connector = new IO.ConnectorDtoSimple(packConnector.Connector);
            ConnectorCount = packConnector.ConnectorCount;
        }
    }

    public class PackConnectorDbo
    {
        [Required]
        public required int ConnectorID { get; set; }
        [Required]
        public required int ConnectorCount { get; set; }
    }

    [PrimaryKey(nameof(PackID), nameof(ConnectorID))]
    public class PackConnector
    {

        public int PackID { get; set; }
        public required int ConnectorID { get; set; }
        public required int ConnectorCount { get; set; }


        [DeleteBehavior(DeleteBehavior.Cascade)]
        [JsonIgnore]
        public Pack Pack { get; set; } = null!;
        [DeleteBehavior(DeleteBehavior.Cascade)]
        public IO.Connector Connector { get; set; } = null!;
    }
}
