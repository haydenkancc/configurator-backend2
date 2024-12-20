﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class LayoutPanelRadiatorDto
    {
        public Cooler.RadiatorSizeDto RadiatorSize { get; set; }

        public LayoutPanelRadiatorDto(LayoutPanelRadiator layoutPanelRadiator)
        {
            RadiatorSize = new Cooler.RadiatorSizeDto(layoutPanelRadiator.RadiatorSize);
        }
    }

    public class LayoutPanelRadiatorDbo
    {
        [Required]
        public required int RadiatorSizeID { get; set; }
    }

    [PrimaryKey(nameof(LayoutPanelID), nameof(RadiatorSizeID))]
    public class LayoutPanelRadiator
    {
        public int LayoutPanelID { get; set; }
        public required int RadiatorSizeID { get; set; }

        [JsonIgnore]
        public LayoutPanel LayoutPanel { get; set; } = null!;
        public Cooler.RadiatorSize RadiatorSize { get; set; } = null!;
    }
}
