using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class LayoutDbo
    {
        [Required]
        public required ICollection<LayoutPanelDbo> Panels { get; set; }
        [Required]
        public required ICollection<StorageArea> StorageAreas { get; set; }
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
