﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Pcie
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
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Version
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Slot> PcieSlots { get; set; } = new List<Slot>();
        [JsonIgnore]
        public ICollection<ExpansionCard> PcieExpansionCards { get; set; } = new List<ExpansionCard>();

        [JsonIgnore]
        public ICollection<M2.ExpansionCard> M2ExpansionCards { get; set; } = new List<M2.ExpansionCard>();
        [JsonIgnore]
        public ICollection<M2.Slot> M2Slots { get; set; } = new List<M2.Slot>();
    }
}
