using ConfiguratorBackend.Models.Catalogue.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfiguratorBackend.Models.Catalogue.Cooler
{
    public class AirUnitDto : BaseUnitDto
    {
        public decimal Height { get; set; }
        public bool LimitsMemoryHeight { get; set; }
        public int? MaximumMemoryHeight { get; set; }

        public AirUnitDto(ComponentDto component, AirUnit unit) : base(component, unit)
        {
            Height = unit.Height;
            LimitsMemoryHeight = unit.LimitsMemoryHeight;
            if (unit.LimitsMemoryHeight)
            {
                MaximumMemoryHeight = unit.MaximumMemoryHeight;
            }
        }
    }

    public class AirUnitDbo : BaseUnitDbo
    {
        [Required]
        public required decimal Height { get; set; }
        [Required]
        public required bool LimitsMemoryHeight { get; set; }
        [Required]
        public required int? MaximumMemoryHeight { get; set; }
    }

    public class AirUnit : Unit
    {
        [Column(TypeName = "decimal(8,2)")]
        public required decimal Height { get; set; }
        public required bool LimitsMemoryHeight { get; set; }
        public required int? MaximumMemoryHeight { get; set; }
    }
}
