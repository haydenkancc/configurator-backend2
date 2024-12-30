using Microsoft.EntityFrameworkCore;
using ConfiguratorBackend.Models.Catalogue.General;
using Pcie = ConfiguratorBackend.Models.Catalogue.Pcie;
using M2 = ConfiguratorBackend.Models.Catalogue.M2;
using IO = ConfiguratorBackend.Models.Catalogue.IO;
using Fan = ConfiguratorBackend.Models.Catalogue.Fan;
using Memory = ConfiguratorBackend.Models.Catalogue.Memory;
using PowerSupply = ConfiguratorBackend.Models.Catalogue.PowerSupply;
using Motherboard = ConfiguratorBackend.Models.Catalogue.Motherboard;
using Storage = ConfiguratorBackend.Models.Catalogue.Storage;
using Cooler = ConfiguratorBackend.Models.Catalogue.Cooler;
using CentralProcessor = ConfiguratorBackend.Models.Catalogue.CentralProcessor;
using GraphicsCard = ConfiguratorBackend.Models.Catalogue.GraphicsCard;
using Case = ConfiguratorBackend.Models.Catalogue.Case;
using ConfiguratorBackend.Models.Catalogue;

namespace Configurator.Data
{
    public class CatalogueContext : DbContext
    {
        public CatalogueContext(DbContextOptions<CatalogueContext> options)
            : base(options)
        {
        }

        public DbSet<Component> Components { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Colour> Colours { get; set; }

        public DbSet<Pcie.Bracket> PcieBrackets { get; set; }
        public DbSet<Pcie.Version> PcieVersions { get; set; }
        public DbSet<Pcie.Size> PcieSizes { get; set; }
        public DbSet<Pcie.Slot> PcieSlots { get; set; }
        public DbSet<Pcie.ExpansionCard> PcieExpansionCards { get; set; }
        
        public DbSet<M2.Key> M2Keys { get; set; }
        public DbSet<M2.FormFactor> M2FormFactors { get; set; }
        public DbSet<M2.Slot> M2Slots { get; set; }
        public DbSet<M2.ExpansionCard> M2ExpansionCards { get; set; }

        public DbSet<IO.Connector> IOConnectors { get; set; }

        public DbSet<Memory.FormFactor> MemoryFormFactors { get; set; }
        public DbSet<Memory.Type> MemoryTypes { get; set; }
        public DbSet<Memory.Kit> MemoryKits { get; set; }

        public DbSet<PowerSupply.Connector> PowerSupplyConnectors { get; set; }
        public DbSet<PowerSupply.EfficiencyRating> PowerSupplyEfficiencyRatings { get; set; }
        public DbSet<PowerSupply.FormFactor> PowerSupplyFormFactors { get; set; }
        public DbSet<PowerSupply.Modularity> PowerSupplyModularities { get; set; }
        public DbSet<PowerSupply.Unit> PowerSupplyUnits { get; set; }
        public DbSet<PowerSupply.UnitConnector> PowerSupplyUnitConnectors { get; set; }
        
        public DbSet<Fan.Size> FanSizes { get; set; }
        public DbSet<Fan.Pack> FanPacks { get; set; }
        
        public DbSet<GraphicsCard.Chipset> GraphicsCardChipsets { get; set; }
        public DbSet<GraphicsCard.Unit> GraphicsCardUnits { get; set; }
        public DbSet<GraphicsCard.Configuration> GraphicsCardConfigurations { get; set; }
        public DbSet<GraphicsCard.ConfigurationConnector> GraphicsCardConfigurationConnectors { get; set; }
        public DbSet<CentralProcessor.Series> CentralProcessorSeries { get; set; }
        public DbSet<CentralProcessor.Socket> CentralProcessorSockets { get; set; }
        public DbSet<CentralProcessor.Microarchitecture> CentralProcessorMicroarchitectures { get; set; }
        public DbSet<CentralProcessor.CoreFamily> CentralProcessorCoreFamilies { get; set; }
        public DbSet<CentralProcessor.Unit> CentralProcessorUnits { get; set; }

        public DbSet<Case.Unit> CaseUnits { get; set; }
        public DbSet<Case.Panel> CasePanels { get; set; }
        public DbSet<Case.Material> CaseMaterials { get; set; }
        public DbSet<Case.Size> CaseSizes { get; set; }
        public DbSet<Case.ExpansionSlotArea> CaseExpansionSlotAreas { get; set; }
        public DbSet<Case.Layout> CaseLayouts { get; set; }
        public DbSet<Case.LayoutPanel> CaseLayoutPanels { get; set; }
        public DbSet<Case.LayoutPanelFan> CaseLayoutPanelFans { get; set; }
        public DbSet<Case.LayoutPanelRadiator> CaseLayoutPanelRadiators { get; set; }
        public DbSet<Case.StorageArea> CaseStorageAreas { get; set; }
        public DbSet<Case.DriveBay> CaseDriveBays { get; set; }
        public DbSet<Case.UnitIOConnector> CaseUnitIOConnectors { get; set; }
        public DbSet<Case.UnitPowerSupplyConnector> CaseUnitPowerSupplyConnectors { get; set; }
        
        public DbSet<Cooler.RadiatorSize> CoolerRadiatorSizes { get; set; }
        public DbSet<Cooler.Unit> CoolerUnits { get; set; }
        public DbSet<Cooler.LiquidUnit> CoolerLiquidUnits { get; set; }
        public DbSet<Cooler.AirUnit> CoolerAirUnits { get; set; }
        public DbSet<Cooler.UnitConnector> CoolerUnitConnectors { get; set; }

        public DbSet<Storage.FormFactor> StorageFormFactors { get; set; }
        public DbSet<Storage.ConnectionInterface> StorageConnectionInterfaces { get; set; }
        public DbSet<Storage.NandType> StorageNandTypes { get; set; }
        public DbSet<Storage.Drive> StorageDrives { get; set; }
        public DbSet<Storage.HardDiskDrive> StorageHardDiskDrives { get; set; }
        public DbSet<Storage.SolidStateDrive> StorageSolidStateDrives { get; set; }
        public DbSet<Storage.Unit> StorageUnits { get; set; }
        public DbSet<Storage.CaseUnit> StorageCaseUnits { get; set; }
        public DbSet<Storage.M2Unit> StorageM2Units { get; set; }

        public DbSet<Motherboard.Chipset> MotherboardChipsets { get; set; }
        public DbSet<Motherboard.FormFactor> MotherboardFormFactors { get; set; }
        public DbSet<Motherboard.Unit> MotherboardUnits { get; set; }
        public DbSet<Motherboard.UnitIOConnector> MotherboardUnitIOConnectors { get; set; }
        public DbSet<Motherboard.UnitPowerSupplyConnector> MotherboardUnitPowerSupplyConnectors { get; set; }
        public DbSet<Motherboard.UnitM2Slot> MotherboardUnitM2Slots { get; set; }
        public DbSet<Motherboard.UnitPcieSlot> MotherboardUnitPcieSlots { get; set; }

        public DbSet<Configuration> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Storage.Unit>()
                .ToTable("StorageUnits")
                .HasDiscriminator<Storage.Location>(nameof(Storage.Unit.Location))
                .HasValue<Storage.CaseUnit>(Storage.Location.Case)
                .HasValue<Storage.M2Unit>(Storage.Location.M2);

            modelBuilder.Entity<Storage.Drive>()
                .ToTable("StorageDrives")
                .HasDiscriminator<Storage.Type>(nameof(Storage.Drive.Type))
                .HasValue<Storage.SolidStateDrive>(Storage.Type.SolidStateDrive)
                .HasValue<Storage.HardDiskDrive>(Storage.Type.HardDiskDrive);

            modelBuilder.Entity<Cooler.Unit>()
                .ToTable("CoolerUnits")
                .HasDiscriminator<Cooler.Type>(nameof(Cooler.Unit.Type))
                .HasValue<Cooler.AirUnit>(Cooler.Type.Air)
                .HasValue<Cooler.LiquidUnit>(Cooler.Type.Liquid);
        }

    }
}
