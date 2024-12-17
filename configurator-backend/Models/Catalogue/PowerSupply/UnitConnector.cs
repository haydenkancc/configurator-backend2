﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.PowerSupply
{
    public class UnitConnectorDbo
    {
        [Required]
        public required int ConnectorID { get; set; }
        [Required]
        public required int ConnectorCount { get; set; }
        [Required]
        public required int SplitCount { get; set; }
    }

    [PrimaryKey(nameof(UnitID), nameof(ConnectorID), nameof(SplitCount))]
    public class UnitConnector
    {
        public int UnitID { get; set; }
        public required int ConnectorID { get; set; }
        public required int ConnectorCount { get; set; }
        public required int SplitCount { get; set; }

        [JsonIgnore]
        public Unit Unit { get; set; } = null!;

        public Connector Connector { get; set; } = null!;
    }
}