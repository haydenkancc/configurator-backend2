using ConfiguratorBackend.Models.Catalogue.IO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.GraphicsCard
{
    public class ConfigurationDbo
    {
        [Required]
        public required ICollection<ConfigurationConnector> Connectors { get; set; }
    }

    public class Configuration
    {
        public int ID { get; set; }
        public required int UnitID { get; set; }

        [JsonIgnore]
        public Unit Unit { get; set; } = null!;

        public ICollection<ConfigurationConnector> Connectors { get; set; } = new List<ConfigurationConnector>();
    }
}
