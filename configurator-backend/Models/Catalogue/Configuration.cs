namespace ConfiguratorBackend.Models.Catalogue
{
    public class Configuration
    {
        public int ID { get; set; }

        public int? CentralProcessorID { get; set }
        public CentralProcessor.Unit? CentralProcessor { get; set; }
        public int? CoolerID { get; set; }
        public Cooler.Unit? Cooler { get; set; }
        public int? MotherboardID { get; set; }
        public Motherboard.Unit? Motherboard { get; set; }
        public ICollection<Memory.Kit> Memory { get; set; } = new List<Memory.Kit>();
        public ICollection<Storage.Unit> Storage { get; set; } = new List<Storage.Unit>();
        public ICollection<GraphicsCard.Unit> GraphicsCards { get; set; } = new List<GraphicsCard.Unit>();
        public int? CaseID { get; set; }
        public Case.Unit? Case { get; set; }
        public int? PowerSupplyID { get; set; }
        public PowerSupply.Unit? PowerSupply { get; set; }
        public ICollection<Fan.Pack> Fans { get; set; } = new List<Fan.Pack>(); 
    }
}
