using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class LayoutPanelFanDbo
    {
        [Required]
        public required int FanSizeID { get; set; }
        [Required]
        public required int FanCount { get; set; }
    }

    [PrimaryKey(nameof(LayoutPanelID), nameof(FanSizeID))]
    public class LayoutPanelFan
    {
        public int LayoutPanelID { get; set; }
        public required int FanSizeID { get; set; }
        public required int FanCount { get; set; }

        [JsonIgnore]
        public LayoutPanel LayoutPanel { get; set; } = null!;
        public Fan.Size FanSize { get; set; } = null!;

    }
}
