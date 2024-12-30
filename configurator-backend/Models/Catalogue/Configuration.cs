namespace ConfiguratorBackend.Models.Catalogue
{
    public class Configuration
    {
        public int ID { get; set; }
        public int? CentralProcessorID { get; set; }
        public ConfigurationCentralProcessor? CentralProcessor { get; set; }
        public int? CoolerID { get; set; }
        public ConfigurationCooler? Cooler { get; set; }
        public int? MotherboardID { get; set; }
        public ConfigurationMotherboard? Motherboard { get; set; }
        public ICollection<ConfigurationMemory> Memory { get; set; } = new List<ConfigurationMemory>();
        public ICollection<ConfigurationStorage> Storage { get; set; } = new List<ConfigurationStorage>();
        public ICollection<ConfigurationGraphicsCard> GraphicsCards { get; set; } = new List<ConfigurationGraphicsCard>();
        public int? CaseID { get; set; }
        public ConfigurationCase? Case { get; set; }
        public int? PowerSupplyID { get; set; }
        public ConfigurationPowerSupply? PowerSupply { get; set; }
        public ICollection<ConfigurationFan> Fans { get; set; } = new List<ConfigurationFan>();
    }

    public class ConfigurationCentralProcessor
    {
        public int ID { get; set; }
        public int ConfigurationID { get; set; }
        public int UnitID { get; set; }
        public int AvailableMemoryCapacity { get; set; }
        public CentralProcessor.Unit Unit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;

    }

    public class ConfigurationCooler
    {
        public int ID { get; set; }
        public int ConfigurationID { get; set; }
        public int UnitID { get; set; }

        public Cooler.Unit Unit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }

    public class ConfigurationPowerSupply
    {
        public int ID { get; set; }
        public int ConfigurationID { get; set; }
        public int UnitID { get; set; }

        /* Key is ConnectorID, Each item in ICollection represents a connector, with the item's value representing the number of currently available splits */
        public Dictionary<int, ICollection<int>> Connectors { get; set; } = new Dictionary<int, ICollection<int>>();

        public PowerSupply.Unit Unit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }

    public class ConfigurationPowerSupplyConnection
    {
        /* References a key in the Dictionary */
        public int ConnectorID { get; set; }
        /* References an index in the ICollection value of the Dictionary */
        public int ConnectionIndex { get; set; }
        /* The value that is subtracted from the ICollection item */
        public int SplitsUsed { get; set; }
    }

    public class ConfigurationMotherboard
    {
        public int ID { get; set; }
        public int ConfigurationID { get; set; }
        public int UnitID { get; set; }
        public int AvailableMemorySlotCount { get; set; }
        public int AvailableMemoryCapacity { get; set; }
        public Dictionary<int, ICollection<int>> IOConnectors { get; set; } = new Dictionary<int, ICollection<int>>();
        public ICollection<ConfigurationPowerSupplyConnection> PowerSupplyConnectionsUsed { get; set; } = new List<ConfigurationPowerSupplyConnection>();
        public Motherboard.Unit Unit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }

    public class ConfigurationIOConnection
    {
        public int ConnectorID { get; set; }
        public int ConnectionIndex { get; set; }
        public int SplitsUsed { get; set; }
    }


    public class ConfigurationGraphicsCard
    {
        public int ID { get; set; }
        public int ConfigurationID {get; set;}
        public int UnitID { get; set; }
        public ICollection<ConfigurationPowerSupplyConnection> PowerSupplyConnectionsUsed { get; set; } = new List<ConfigurationPowerSupplyConnection>();


        public GraphicsCard.Unit Unit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }

    public class ConfigurationStorage
    {
        public int ID { get; set; }
        public int ConfigurationID { get; set; }
        public int UnitID { get; set; }
        public ICollection<ConfigurationPowerSupplyConnection> PowerSupplyConnectionsUsed { get; set; } = new List<ConfigurationPowerSupplyConnection>();
        public ICollection<ConfigurationIOConnection> IOConnectionsUsed { get; set; } = new List<ConfigurationIOConnection>();

        public Storage.Unit Unit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }

    public class ConfigurationMemory
    {
        public int ID { get; set; }
        public int ConfigurationID { get; set; }
        public int KitID { get; set; }

        public Memory.Kit Kit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }

    public class ConfigurationCase
    {
        public int ID {get; set;}
        public int ConfigurationID {get; set;}
        public int UnitID { get; set; }
        public ICollection<ConfigurationPowerSupplyConnection> PowerSupplyConnectionsUsed { get; set; } = new List<ConfigurationPowerSupplyConnection>();
        public ICollection<ConfigurationIOConnection> IOConnectionsUsed { get; set; } = new List<ConfigurationIOConnection>();
        public ICollection<ConfigurationCaseLayout> Layouts { get; set; } = new List<ConfigurationCaseLayout>();
        public Case.Unit Unit { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }

    public class ConfigurationCaseLayout
    {
        public int ID { get; set; }
        public int LayoutID { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<ConfigurationCaseLayoutPanel> LayoutPanels { get; set; } = new List<ConfigurationCaseLayoutPanel>();
        public ICollection<ConfigurationCaseLayoutStorageArea> LayoutStorageAreas { get; set; } = new List<ConfigurationCaseLayoutStorageArea>();
        public Case.Layout Layout { get; set; } = null!;
    }

    public class ConfigurationCaseLayoutPanel
    {
        public int ID { get; set; }
        public int LayoutPanelID { get; set; }
        public ICollection<ConfigurationCaseLayoutPanelFan> Fans { get; set; } = new List<ConfigurationCaseLayoutPanelFan>();
        public ICollection<ConfigurationCaseLayoutPanelRadiator> Radiators { get; set; } = new List<ConfigurationCaseLayoutPanelRadiator>(); 
        public Case.LayoutPanel LayoutPanel { get; set; } = null!;
    }

    public class ConfigurationCaseLayoutPanelFan
    {
        public int ID { get; set; }
        public int PanelFanID { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableCount { get; set; }
        public Case.LayoutPanelFan PanelFan { get; set; } = null!;
    }

    public class ConfigurationCaseLayoutPanelRadiator
    {
        public int ID { get; set; }
        public int PanelRadiatorID { get; set; }
        public bool IsAvailable { get; set; }
        public Case.LayoutPanelRadiator PanelRadiator { get; set; } = null!;
    }

    public class ConfigurationCaseLayoutStorageArea
    {
        public int ID { get; set; }
        public int StorageAreaID { get; set; }
        public ICollection<ConfigurationCaseDriveBay> DriveBays { get; set; } = new List<ConfigurationCaseDriveBay>();

        public Case.StorageArea StorageArea { get; set; } = null!;
    }

    public class ConfigurationCaseDriveBay
    {
        public int ID { get; set; }
        public int DriveBayID { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableDriveCount { get; set; }
        public Case.DriveBay DriveBay { get; set; } = null!;
    }

    public class ConfigurationFan
    {
        public int ID {get; set;}
        public int ConfigurationID {get; set;}
        public int PackID { get; set; }
        public ICollection<ConfigurationIOConnection> IOConnectionsUsed { get; set; } = new List<ConfigurationIOConnection>();
        public Fan.Pack Pack { get; set; } = null!;
        public Configuration Configuration { get; set; } = null!;
    }




}
