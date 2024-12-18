using ConfiguratorBackend.Models.Catalogue.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguratorBackend.Models.Catalogue.Cooler
{
    public class LiquidUnitDto : BaseUnitDto
    {
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public RadiatorSizeDto RadiatorSize { get; set; }

        public LiquidUnitDto(ComponentDto component, LiquidUnit unit) : base(component, unit)
        {
            Length = unit.Length;
            Width = unit.Width;
            Height = unit.Height;
            RadiatorSize = new RadiatorSizeDto(unit.RadiatorSize);
        }
    }

    public class LiquidUnitDbo : BaseUnitDbo
    {
        [Required]
        public required int RadiatorSizeID { get; set; }
        [Required]
        public required decimal Length { get; set; }
        [Required]
        public required decimal Width { get; set; }
        [Required]
        public required decimal Height { get; set; }
    }

    public class LiquidUnit : Unit
    {
        public required int RadiatorSizeID { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Length { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Width { get; set; }
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Height { get; set; }

        public RadiatorSize RadiatorSize { get; set; } = null!;
    }
}
