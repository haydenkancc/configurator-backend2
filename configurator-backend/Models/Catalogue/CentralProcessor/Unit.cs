using configurator_backend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.CentralProcessor
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

    public class CentralProcessorUnitListItem : ComponentListItem
    {
        public int CoreCount { get; set; }
        public string PerformanceCoreClock { get; set; }
        public string PerformanceCoreBoostClock { get; set; }
        public string TotalPower { get; set; }
        public string Microarchitecture { get; set; }
        public string CoreFamily { get; set; }
        public string IntegratedGraphics { get; set; }

        public CentralProcessorUnitListItem(Unit unit) : base(unit.Component)
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

    public class CentralProcessorUnitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<Socket> Sockets { get; set; }
        public required ICollection<Series> Series { get; set; }
        public required ICollection<Channel> Channels { get; set; }
        public required ICollection<CoreFamily> CoreFamilies { get; set; }
    }

    public class CentralProcessorUnitDto
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


        public CentralProcessorUnitDto(ComponentDto component, Unit unit)
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

    public class CentralProcessorUnitDbo
    {
        public required ComponentDbo Component { get; set; }
        public int SocketID { get; set; }
        public int SeriesID { get; set; }
        public int ChannelID { get; set; }
        public int CoreFamilyID { get; set; }
        public int MaxTotalMemoryCapacityID { get; set; }
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
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public int ComponentID { get; set; }
        public int SocketID { get; set; }
        public int SeriesID { get; set; }
        public int ChannelID { get; set; }
        public int CoreFamilyID { get; set; }

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
        [Column(TypeName = "decimal(6,2)")]
        public decimal PerformanceCoreClock { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal PerformanceCoreBoostClock { get; set; }
        public bool HasEfficiencyCores { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal EfficiencyCoreClock { get; set; } = 0m;
        [Column(TypeName = "decimal(6,2)")]
        public decimal EfficiencyCoreBoostClock { get; set; } = 0m;
        [Column(TypeName = "decimal(10,2)")]
        public decimal L2Cache { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal L3Cache { get; set; }
        public bool SimultaneousMultithreading { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public required Component Component { get; set; }
        public required CoreFamily CoreFamily { get; set; }


        public required Socket Socket { get; set; }
        public required Series Series { get; set; }
        public required Channel Channel { get; set; }
    }
}
