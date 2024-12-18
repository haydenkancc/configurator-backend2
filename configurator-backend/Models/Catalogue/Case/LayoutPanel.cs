using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class LayoutPanelDto
    {
        public ICollection<LayoutPanelRadiatorDto> Radiators { get; set; }
        public ICollection<LayoutPanelFanDto> Fans { get; set; }

        public LayoutPanelDto(LayoutPanel layoutPanel)
        {
            Radiators = layoutPanel.Radiators.Select(e => new LayoutPanelRadiatorDto(e)).ToList();
            Fans = layoutPanel.Fans.Select(e => new LayoutPanelFanDto(e)).ToList();
        }
    }

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
        public int LayoutID { get; set; }
        public required int PanelID { get; set; }

        public required ICollection<LayoutPanelRadiator> Radiators { get; set; }
        public required ICollection<LayoutPanelFan> Fans { get; set; }

        [JsonIgnore]
        public Layout Layout { get; set; } = null!;
        public Panel Panel { get; set; } = null!;
    }
}
