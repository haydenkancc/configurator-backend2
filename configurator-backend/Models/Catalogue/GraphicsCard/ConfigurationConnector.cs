﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.GraphicsCard
{
    public class ConfigurationConnectorDto
    {
        public PowerSupply.ConnectorDtoSimple Connector { get; set; }
        public int ConnectorCount { get; set; }

        public ConfigurationConnectorDto(ConfigurationConnector unitConnector)
        {
            Connector = new PowerSupply.ConnectorDtoSimple(unitConnector.Connector);
            ConnectorCount = unitConnector.ConnectorCount;
        }
    }
    public class ConfigurationConnectorDbo
    {
        [Required]
        public int ConnectorID { get; set; }
        [Required]
        public int ConnectorCount { get; set; }
    }

    [PrimaryKey(nameof(ConfigurationID), nameof(ConnectorID))]
    public class ConfigurationConnector
    {
        public int ConfigurationID { get; set; }
        public required int ConnectorID { get; set; }
        public required int ConnectorCount { get; set; }

        [JsonIgnore]
        public Configuration Configuration { get; set; } = null!;

        public PowerSupply.Connector Connector { get; set; } = null!;
    }
}
