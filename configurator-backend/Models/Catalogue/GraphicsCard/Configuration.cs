using ConfiguratorBackend.Models.Catalogue.IO;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.GraphicsCard
{
    public class ConfigurationDto
    {
        public ICollection<ConfigurationConnectorDto> Connectors { get; set; }

        public ConfigurationDto(Configuration configuration)
        {
            Connectors = configuration.Connectors.Select(e => new ConfigurationConnectorDto(e)).ToList();
        }
    }
    
    public class ConfigurationDbo
    {
        [Required]
        public required ICollection<ConfigurationConnectorDbo> Connectors { get; set; }
    }

    public class Configuration
    {
        public int ID { get; set; }
        public int UnitID { get; set; }

        [JsonIgnore]
        public Unit Unit { get; set; } = null!;

        public ICollection<ConfigurationConnector> Connectors { get; set; } = new List<ConfigurationConnector>();
    }
}
