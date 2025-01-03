﻿using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguratorBackend.Models.Catalogue.Cooler
{
    public enum Type
    {
        Air = 0,
        Liquid = 1,
    }

    public class UnitListItem : ComponentListItem
    {
        public string FanRpm { get; set; }
        public string FanAirflow { get; set; }
        public string FanNoiseLevel { get; set; }
        public UnitListItem(Unit unit) : base (unit.Component)
        {
            FanRpm = unit.FanRpm ?? "N/A";
            FanAirflow = unit.FanAirflow ?? "N/A";
            FanNoiseLevel = unit.FanNoiseLevel ?? "N/A";

        }
    }

    public class UnitDto
    {
        public Type Type { get; set; }
        public LiquidUnitDto? LiquidUnit { get; set; }
        public AirUnitDto? AirUnit { get; set; }

        public UnitDto(Type type, LiquidUnitDto? liquidUnit)
        {
            Type = type;
            LiquidUnit = liquidUnit;
            AirUnit = null;
        }
        public UnitDto(Type type, AirUnitDto? airUnit)
        {
            Type = type;
            AirUnit = airUnit;
            LiquidUnit = null;
        }

    }

    public abstract class BaseUnitDto
    {
        public ComponentDto Component { get; set; }
        public ICollection<CentralProcessor.SocketDto> Sockets { get; set; }
        public ICollection<UnitConnectorDto>? Connectors { get; set; }
        public bool IsPassive { get; set; }
        public int? FanCount { get; set; }
        public string? FanRpm { get; set; }
        public string? FanAirflow { get; set; }
        public string? FanNoiseLevel { get; set; }
        public string? FanStaticPressure { get; set; }

        public BaseUnitDto(ComponentDto component, Unit unit)
        {
            Component = component;
            Sockets = unit.Sockets.Select(e => new CentralProcessor.SocketDto(e)).ToList();
            Connectors = unit.Connectors.Select(e => new UnitConnectorDto(e)).ToList();
            IsPassive = unit.IsPassive;
            if (!unit.IsPassive)
            {
                FanCount = unit.FanCount;
                FanRpm = unit.FanRpm;
                FanAirflow = unit.FanAirflow;
                FanNoiseLevel = unit.FanNoiseLevel;
                FanStaticPressure = unit.FanStaticPressure;
            }
        }
    }

    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<CentralProcessor.SocketDto> Sockets { get; set; }
        public required ICollection<IO.ConnectorDtoSimple> Connectors { get; set; }
        public required ICollection<RadiatorSizeDto> RadiatorSizes { get; set; }
    }

    public class UnitDbo
    {
        [Required]
        public required Type Type { get; set; }
        [Required]
        public required LiquidUnitDbo? LiquidUnit { get; set; }
        [Required]
        public required AirUnitDbo? AirUnit { get; set; }
    }

    public abstract class BaseUnitDbo
    {
        [Required]
        public required ComponentDbo Component { get; set; }
        [Required]
        public required ICollection<UnitConnectorDbo> Connectors { get; set; }
        [Required]
        public required ICollection<int> SocketIDs { get; set; }
        [Required]
        public required bool IsPassive { get; set; }
        [Required]
        public int? FanCount { get; set; }
        [Required]
        public string? FanRpm { get; set; }
        [Required]
        public string? FanAirflow { get; set; }
        [Required]
        public string? FanNoiseLevel { get; set; }
        [Required]
        public string? FanStaticPressure { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public abstract class Unit
    {
        public int ComponentID { get; set; }
        public Type Type { get; set; }

        public required bool IsPassive { get; set; }
        public required int? FanCount { get; set; }
        public required string? FanRpm { get; set; }
        public required string? FanAirflow { get; set; }
        public required string? FanNoiseLevel { get; set; }
        public required string? FanStaticPressure { get; set; }


        public required ICollection<CentralProcessor.Socket> Sockets { get; set; }
        public ICollection<UnitConnector> Connectors { get; set; } = new List<UnitConnector>();

        [ForeignKey(nameof(ComponentID))]
        public required Component Component { get; set; }
    }
}
