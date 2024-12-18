using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class UnitPowerSupplyConnectorDto
    {
        public PowerSupply.ConnectorDtoSimple Connector { get; set; }
        public int ConnectorCount { get; set; }

        public UnitPowerSupplyConnectorDto(UnitPowerSupplyConnector unitConnector)
        {
            Connector = new PowerSupply.ConnectorDtoSimple(unitConnector.Connector);
            ConnectorCount = unitConnector.ConnectorCount;
        }
    }

    public class UnitPowerSupplyConnectorDbo
    {
        [Required]
        public required int ConnectorID { get; set; }
        [Required]
        public required int ConnectorCount { get; set; }
    }

    [PrimaryKey(nameof(UnitID), nameof(ConnectorID))]
    public class UnitPowerSupplyConnector
    {
        public int UnitID { get; set; }
        public required int ConnectorID { get; set; }
        public required int ConnectorCount { get; set; }

        [JsonIgnore]
        public Unit Unit { get; set; } = null!;
        public PowerSupply.Connector Connector { get; set; } = null!;
    }
}
