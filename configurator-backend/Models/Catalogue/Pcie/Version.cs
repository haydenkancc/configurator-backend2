using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Pcie
{
    public class VersionListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public VersionListItem(Version version)
        {
            ID = version.ID;
            Name = version.Name;
        }
    }

    public class VersionDto(Version version)
    {
        public int ID { get; set; } = version.ID;
        public string Name { get; set; } = version.Name;
    }

    public class VersionDbo
    {
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Version
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Slot>? PcieSlots { get; set; }
        [JsonIgnore]
        public ICollection<ExpansionCard>? PcieExpansionCards { get; set; }

        [JsonIgnore]
        public ICollection<M2.ExpansionCard>? M2ExpansionCards { get; set; }
        [JsonIgnore]
        public ICollection<M2.Slot>? M2Slots { get; set; }
    }
}
