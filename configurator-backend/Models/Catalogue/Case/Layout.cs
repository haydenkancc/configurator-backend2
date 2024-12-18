using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class LayoutDto
    {
        public ICollection<LayoutPanelDto> Panels { get; set; }
        public ICollection<StorageAreaDto> StorageAreas { get; set; }
        public decimal MaxPowerSupplyLength { get; set; }
        public decimal MaxAirCoolerHeight { get; set; }
        public decimal MaxGraphicsProcessorUnitLength { get; set; }

        public LayoutDto(Layout layout)
        {
            Panels = layout.Panels.Select(e => new LayoutPanelDto(e)).ToList();
            StorageAreas = layout.StorageAreas.Select(e => new StorageAreaDto(e)).ToList();
            MaxPowerSupplyLength = layout.MaxPowerSupplyLength;
            MaxAirCoolerHeight = layout.MaxAirCoolerHeight;
            MaxGraphicsProcessorUnitLength = layout.MaxGraphicsProcessorUnitLength;
        }
    }

    public class LayoutDbo
    {
        [Required]
        public required ICollection<LayoutPanelDbo> Panels { get; set; }
        [Required]
        public required ICollection<StorageAreaDbo> StorageAreas { get; set; }
        [Required]
        public required decimal MaxPowerSupplyLength { get; set; }
        [Required]
        public required decimal MaxAirCoolerHeight { get; set; }
        [Required]
        public required decimal MaxGraphicsProcessorUnitLength { get; set; }

    }

    [PrimaryKey(nameof(UnitID))]
    public class Layout
    {
        public int UnitID { get; set; }


        [Column(TypeName = "decimal(8,2)")]
        public required decimal MaxPowerSupplyLength { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal MaxAirCoolerHeight { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal MaxGraphicsProcessorUnitLength { get; set; }

        public required ICollection<LayoutPanel> Panels { get; set; }
        public required ICollection<StorageArea> StorageAreas { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UnitID))]
        public Unit Unit { get; set; } = null!;
    }
}
