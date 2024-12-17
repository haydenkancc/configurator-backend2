using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class UnitListItem : ComponentListItem
    {
        public string Type { get; set; }
        public string SidePanel { get; set; }
        public string ExternalVolume { get; set; }

        public UnitListItem(Unit unit) : base(unit.Component)
        {
            Type = $"{unit.PrimaryFormFactor.Name} {unit.Size.Name}";
            SidePanel = unit.SidePanelMaterial.Name;
            ExternalVolume = $"{unit.ExternalVolume} L";
        }
    }

    public class UnitDto
    {
        public ComponentDto Component { get; set; }
        public ICollection<Layout> Layouts { get; set; }
        public ICollection<UnitIOConnector>? IOConnectors { get; set; }
        public ICollection<UnitPowerSupplyConnector>? PowerSupplyConnectors { get; set; }
        public Motherboard.FormFactor PrimaryFormFactor { get; set; }
        public ICollection<Motherboard.FormFactor> MotherboardFormFactors { get; set; }
        public PowerSupply.FormFactor PowerSupplyFormFactor { get; set; }
        public ICollection<ExpansionSlotArea>? ExpansionSlotAreas { get; set; }
        public Size Size { get; set; }
        public Material SidePanelMaterial { get; set; }

        public decimal ExternalVolume { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public UnitDto(ComponentDto component, Unit unit)
        {
            Component = component;
            Layouts = unit.Layouts;
            IOConnectors = unit.IOConnectors;
            PowerSupplyConnectors = unit.PowerSupplyConnectors;
            PrimaryFormFactor = unit.PrimaryFormFactor;
            MotherboardFormFactors = unit.MotherboardFormFactors;
            PowerSupplyFormFactor = unit.PowerSupplyFormFactor;
            ExpansionSlotAreas = unit.ExpansionSlotAreas;
            Size = unit.Size;
            SidePanelMaterial = unit.SidePanelMaterial;
            ExternalVolume = unit.ExternalVolume;
            Length = unit.Length;
            Width = unit.Width;
            Height = unit.Height;
        }
    }

    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<PowerSupply.FormFactor> PowerSupplyFormFactors { get; set; }
        public required ICollection<Motherboard.FormFactor> MotherboardFormFactors { get; set; }
        public required ICollection<Size> Sizes { get; set; }
        public required ICollection<Material> Materials { get; set; }
        public required ICollection<Cooler.RadiatorSize> RadiatorSizes { get; set; }
        public required ICollection<Fan.Size> FanSizes { get; set; }
        public required ICollection<Pcie.Bracket> Brackets { get; set; }
        public required ICollection<Panel> Panels { get; set; }
        public required ICollection<IO.Connector> IOConnectors { get; set; }
        public required ICollection<PowerSupply.Connector> PowerSupplyConnectors { get; set; }
        public required ICollection<Storage.FormFactor> StorageFormFactors { get; set; }
    }

    public class UnitDbo
    {
        
        [Required]
        public required ComponentDbo Component { get; set; }
        
        [Required]
        public required int PowerSupplyFormFactorID { get; set; }
        
        [Required]
        public required int PrimaryFormFactorID { get; set; }
        
        [Required]
        public required int SizeID { get; set; }
        
        [Required]
        public required int SidePanelMaterialID { get; set; }
        
        [Required]
        public required decimal ExternalVolume { get; set; }
        
        [Required]
        public required decimal Length { get; set; }
        
        [Required]
        public required decimal Width { get; set; }
        
        [Required]
        public required decimal Height { get; set; }
        
        [Required]
        public required ICollection<LayoutDbo> Layouts { get; set; }
        
        [Required]
        public required ICollection<UnitIOConnectorDbo> IOConnectors { get; set; }
        
        [Required]
        public required ICollection<UnitPowerSupplyConnectorDbo> PowerSupplyConnectors { get; set; }
        
        [Required]
        public required ICollection<int> MotherboardFormFactorIDs { get; set; }
        
        [Required]
        public required ICollection<ExpansionSlotAreaDbo> ExpansionSlotAreas { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public int ComponentID { get; set; }
        public required int PowerSupplyFormFactorID { get; set; }
        public required int PrimaryFormFactorID { get; set; }
        public required int SizeID { get; set; }
        public required int SidePanelMaterialID { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal ExternalVolume { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Length { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Width { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Height { get; set; }


        [ForeignKey(nameof(ComponentID))]
        public Component Component { get; set; } = null!;
        public ICollection<Layout> Layouts { get; set; } = null!;
        public ICollection<UnitIOConnector> IOConnectors { get; set; } = new List<UnitIOConnector>();
        public ICollection<UnitPowerSupplyConnector> PowerSupplyConnectors { get; set; } = new List<UnitPowerSupplyConnector>();
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Motherboard.FormFactor PrimaryFormFactor { get; set; } = null!;
        public ICollection<Motherboard.FormFactor> MotherboardFormFactors { get; set; } = null!;
        public PowerSupply.FormFactor PowerSupplyFormFactor { get; set; } = null!;
        public ICollection<ExpansionSlotArea> ExpansionSlotAreas { get; set; } = new List<ExpansionSlotArea>();
        public Size Size { get; set; } = null!;
        public Material SidePanelMaterial { get; set; } = null!;
    }
}
