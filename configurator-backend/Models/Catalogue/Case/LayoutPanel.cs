using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class LayoutPanelDbo
    {
        [Required]
        public required int PanelID { get; set; }
        [Required]
        public required ICollection<LayoutPanelRadiatorDbo> Radiators { get; set; }
        [Required]
        public required ICollection<LayoutPanelFanDbo> Fans { get; set; }
    }

    [Index(nameof(LayoutID), nameof(PanelID), IsUnique = true)]
    public class LayoutPanel
    {
        public int ID { get; set; }
        public required int LayoutID { get; set; }
        public required int PanelID { get; set; }

        public ICollection<LayoutPanelRadiator> Radiators { get; set; } = new List<LayoutPanelRadiator>();
        public ICollection<LayoutPanelFan> Fans { get; set; } = new List<LayoutPanelFan>();

        [JsonIgnore]
        public Layout Layout { get; set; } = null!;
        public Panel Panel { get; set; } = null!;
    }
}
