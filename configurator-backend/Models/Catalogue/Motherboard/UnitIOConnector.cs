﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Motherboard
{
    public class UnitIOConnectorDto
    {
        public IO.ConnectorDtoSimple Connector { get; set; }
        public int ConnectorCount { get; set; }

        public UnitIOConnectorDto(UnitIOConnector unitConnector)
        {
            Connector = new IO.ConnectorDtoSimple(unitConnector.Connector);
            ConnectorCount = unitConnector.ConnectorCount;
        }
    }

    public class UnitIOConnectorDbo
    {
        [Required]
        public required int ConnectorID { get; set; }
        [Required]
        public required int ConnectorCount { get; set; }
    }

    [PrimaryKey(nameof(UnitID), nameof(ConnectorID))]
    public class UnitIOConnector
    {
        public int UnitID { get; set; }
        public required int ConnectorID { get; set; }
        public required int ConnectorCount { get; set; }

        [JsonIgnore]
        public Unit Unit { get; set; } = null!;
        [JsonIgnore]
        public IO.Connector Connector { get; set; } = null!;
    }
}
