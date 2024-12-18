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
            Type = $"{unit.PrimaryMotherboardFormFactor.Name} {unit.Size.Name}";
            SidePanel = unit.SidePanelMaterial.Name;
            ExternalVolume = $"{unit.ExternalVolume} L";
        }
    }

    public class UnitDto
    {
        public ComponentDto Component { get; set; }
        public ICollection<LayoutDto> Layouts { get; set; }
        public ICollection<UnitIOConnectorDto>? IOConnectors { get; set; }
        public ICollection<UnitPowerSupplyConnectorDto>? PowerSupplyConnectors { get; set; }
        public Motherboard.FormFactorDto PrimaryMotherboardFormFactor { get; set; }
        public ICollection<Motherboard.FormFactorDto> MotherboardFormFactors { get; set; }
        public PowerSupply.FormFactorDto PowerSupplyFormFactor { get; set; }
        public ICollection<ExpansionSlotAreaDto>? ExpansionSlotAreas { get; set; }
        public SizeDto Size { get; set; }
        public MaterialDto SidePanelMaterial { get; set; }

        public decimal ExternalVolume { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public UnitDto(ComponentDto component, Unit unit)
        {
            Component = component;
            Layouts = unit.Layouts.Select(e => new LayoutDto(e)).ToList();
            IOConnectors = unit.IOConnectors.Select(e => new UnitIOConnectorDto(e)).ToList();
            PowerSupplyConnectors = unit.PowerSupplyConnectors.Select(e => new UnitPowerSupplyConnectorDto(e)).ToList();
            PrimaryMotherboardFormFactor = new Motherboard.FormFactorDto(unit.PrimaryMotherboardFormFactor);
            MotherboardFormFactors = unit.MotherboardFormFactors.Select(e => new Motherboard.FormFactorDto(e)).ToList();
            PowerSupplyFormFactor = new PowerSupply.FormFactorDto(unit.PowerSupplyFormFactor);
            ExpansionSlotAreas = unit.ExpansionSlotAreas.Select(e => new ExpansionSlotAreaDto(e)).ToList();
            Size = new SizeDto(unit.Size);
            SidePanelMaterial = new MaterialDto(unit.SidePanelMaterial);
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
        public required int PrimaryMotherboardFormFactorID { get; set; }
        
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
        public required int ComponentID { get; set; }
        public required int PowerSupplyFormFactorID { get; set; }
        public required int PrimaryMotherboardFormFactorID { get; set; }
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
        public required ICollection<Layout> Layouts { get; set; }
        public required ICollection<UnitIOConnector> IOConnectors { get; set; }
        public required ICollection<UnitPowerSupplyConnector> PowerSupplyConnectors { get; set; }
        [DeleteBehavior(DeleteBehavior.Restrict)]
        public Motherboard.FormFactor PrimaryMotherboardFormFactor { get; set; } = null!;
        public required ICollection<Motherboard.FormFactor> MotherboardFormFactors { get; set; }
        public PowerSupply.FormFactor PowerSupplyFormFactor { get; set; } = null!;
        public required ICollection<ExpansionSlotArea> ExpansionSlotAreas { get; set; }
        public Size Size { get; set; } = null!;
        public Material SidePanelMaterial { get; set; } = null!;
    }
}
