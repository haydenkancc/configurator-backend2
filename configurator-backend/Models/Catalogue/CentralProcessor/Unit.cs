﻿using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.CentralProcessor
{
    public class UnitListItemSimple
    {
        public int ComponentID { get; set; }
        public required string Name { get; set; }

        public UnitListItemSimple(Unit unit)
        {
            ComponentID = unit.ComponentID;
            Name = $"{unit.Component.Manufacturer.Name} {unit.Series.Name} {unit.Component.Name}";
        }
    }

    public class UnitListItem : ComponentListItem
    {
        public int CoreCount { get; set; }
        public string PerformanceCoreClock { get; set; }
        public string PerformanceCoreBoostClock { get; set; }
        public string TotalPower { get; set; }
        public string Microarchitecture { get; set; }
        public string CoreFamily { get; set; }
        public string IntegratedGraphics { get; set; }

        public UnitListItem(Unit unit) : base(unit.Component)
        {
            Name = $"{unit.Component.Manufacturer.Name} {unit.Series.Name} {unit.Component.Name}";
            CoreCount = unit.CoreCount;
            PerformanceCoreClock = unit.PerformanceCoreClock.ToString("G2") + " GHz";
            PerformanceCoreBoostClock = unit.PerformanceCoreBoostClock.ToString("G2") + " GHz";
            Microarchitecture = unit.CoreFamily.Microarchitecture.Name;
            CoreFamily = unit.CoreFamily.Name;
            TotalPower = unit.TotalPower.ToString() + " W";
            IntegratedGraphics = unit.HasIntegratedGraphics ? "Yes" : "No";
        }
    }

    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<Socket> Sockets { get; set; }
        public required ICollection<Series> Series { get; set; }
        public required ICollection<Channel> Channels { get; set; }
        public required ICollection<CoreFamily> CoreFamilies { get; set; }
    }

    public class UnitDto
    {
        public ComponentDto Component { get; set; }
        public Socket Socket { get; set; }
        public Series Series { get; set; }
        public Channel Channel { get; set; }
        public CoreFamily CoreFamily { get; set; }

        public int MaxTotalMemoryCapacity { get; set; }
        public int TotalPower { get; set; }
        public bool HasIntegratedGraphics { get; set; }
        public bool CoolerIncluded { get; set; }
        public bool SupportECCMemory { get; set; }
        public bool SupportNonECCMemory { get; set; }
        public bool SupportBufferedMemory { get; set; }
        public bool SupportUnbufferedMemory { get; set; }

        public int CoreCount { get; set; }
        public int ThreadCount { get; set; }
        public decimal PerformanceCoreClock { get; set; }
        public decimal PerformanceCoreBoostClock { get; set; }
        public bool HasEfficiencyCores { get; set; }
        public decimal EfficiencyCoreClock { get; set; }
        public decimal EfficiencyCoreBoostClock { get; set; }
        public decimal L2Cache { get; set; }
        public decimal L3Cache { get; set; }
        public bool SimultaneousMultithreading { get; set; }


        public UnitDto(ComponentDto component, Unit unit)
        {
            Component = component;
            Socket = unit.Socket;
            Series = unit.Series;
            Channel = unit.Channel;
            CoreFamily = unit.CoreFamily;

            MaxTotalMemoryCapacity = unit.MaxTotalMemoryCapacity;
            TotalPower = unit.TotalPower;
            HasIntegratedGraphics = unit.HasIntegratedGraphics;
            CoolerIncluded = unit.CoolerIncluded;
            SupportECCMemory = unit.SupportECCMemory;
            SupportNonECCMemory = unit.SupportNonECCMemory;
            SupportBufferedMemory = unit.SupportBufferedMemory;
            SupportUnbufferedMemory = unit.SupportUnbufferedMemory;
            CoreCount = unit.CoreCount;
            ThreadCount = unit.ThreadCount;
            PerformanceCoreClock = unit.PerformanceCoreClock;
            PerformanceCoreBoostClock = unit.PerformanceCoreBoostClock;
            HasEfficiencyCores = unit.HasEfficiencyCores;
            EfficiencyCoreClock = unit.EfficiencyCoreClock;
            EfficiencyCoreBoostClock = unit.EfficiencyCoreBoostClock;
            L2Cache = unit.L2Cache;
            L3Cache = unit.L3Cache;
            SimultaneousMultithreading = unit.SimultaneousMultithreading;
        }
    }

    public class UnitDbo
    {
        [Required]
        public required ComponentDbo Component { get; set; }
        [Required]
        public required int SocketID { get; set; }
        [Required]
        public required int SeriesID { get; set; }
        [Required]
        public required int ChannelID { get; set; }
        [Required]
        public required int CoreFamilyID { get; set; }
        [Required]
        public required int MaxTotalMemoryCapacityID { get; set; }
        [Required]
        public required int TotalPower { get; set; }
        [Required]
        public required bool HasIntegratedGraphics { get; set; }
        [Required]
        public required bool CoolerIncluded { get; set; }
        [Required]
        public required bool SupportECCMemory { get; set; }
        [Required]
        public required bool SupportNonECCMemory { get; set; }
        [Required]
        public required bool SupportBufferedMemory { get; set; }
        [Required]
        public required bool SupportUnbufferedMemory { get; set; }
        [Required]
        public required int CoreCount { get; set; }
        [Required]
        public required int ThreadCount { get; set; }

        [Required]
        public required decimal PerformanceCoreClock { get; set; }
        [Required]
        public required decimal PerformanceCoreBoostClock { get; set; }
        [Required]
        public required bool HasEfficiencyCores { get; set; }
        [Required]
        public required decimal EfficiencyCoreClock { get; set; }
        [Required]
        public required decimal EfficiencyCoreBoostClock { get; set; }
        [Required]
        public required decimal L2Cache { get; set; }
        [Required]
        public required decimal L3Cache { get; set; }
        [Required]
        public required bool SimultaneousMultithreading { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public int ComponentID { get; set; }
        public required int SocketID { get; set; }
        public required int SeriesID { get; set; }
        public required int ChannelID { get; set; }
        public required int CoreFamilyID { get; set; }

        public required int MaxTotalMemoryCapacity { get; set; }
        public required int TotalPower { get; set; }
        public required bool HasIntegratedGraphics { get; set; }
        public required bool CoolerIncluded { get; set; }
        public required bool SupportECCMemory { get; set; }
        public required bool SupportNonECCMemory { get; set; }
        public required bool SupportBufferedMemory { get; set; }
        public required bool SupportUnbufferedMemory { get; set; }

        public required int CoreCount { get; set; }
        public required int ThreadCount { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public required decimal PerformanceCoreClock { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public required decimal PerformanceCoreBoostClock { get; set; }
        public required bool HasEfficiencyCores { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public required decimal EfficiencyCoreClock { get; set; } = 0m;
        [Column(TypeName = "decimal(6,2)")]
        public required decimal EfficiencyCoreBoostClock { get; set; } = 0m;
        [Column(TypeName = "decimal(10,2)")]
        public required decimal L2Cache { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public required decimal L3Cache { get; set; }
        public required bool SimultaneousMultithreading { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public Component Component { get; set; } = null!;
        public CoreFamily CoreFamily { get; set; } = null!;


        public Socket Socket { get; set; } = null!;
        public Series Series { get; set; } = null!;
        public Channel Channel { get; set; } = null!;
    }
}