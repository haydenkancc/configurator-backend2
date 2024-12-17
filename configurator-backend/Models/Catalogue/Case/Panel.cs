using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class PanelListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public PanelListItem(Panel panel)
        {
            ID = panel.ID;
            Name = panel.Name;
        }
    }

    public class PanelDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public PanelDto(Panel panel)
        {
            ID = panel.ID;
            Name = panel.Name;
        }
    }

    public class PanelDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Panel
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<LayoutPanel> Layouts { get; set; } = new List<LayoutPanel>();
    }
}
