using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Cooler
{
    public class RadiatorSizeListItem
    {
        public int ID { get; set; }
        public int Length { get; set; }

        public RadiatorSizeListItem(RadiatorSize radiatorSize)
        {
            ID = radiatorSize.ID;
            Length = radiatorSize.Length;
        }
    }

    public class RadiatorSizeDto
    {
        public int ID { get; set; }
        public int Length { get; set; }

        public RadiatorSizeDto(RadiatorSize radiatorSize)
        {
            ID = radiatorSize.ID;
            Length = radiatorSize.Length;
        }
    }

    public class RadiatorSizeDbo
    {
        [Required]
        public required int Length { get; set; }
    }

    [Index(nameof(Length), IsUnique = true)]
    public class RadiatorSize
    {
        public int ID { get; set; }

        public required int Length { get; set; }

        [JsonIgnore]
        public ICollection<LiquidUnit> Units { get; set; } = new List<LiquidUnit>();
        [JsonIgnore]
        public ICollection<Case.LayoutPanelRadiator> CasePanels { get; set; } = new List<Case.LayoutPanelRadiator>();
    }
}
