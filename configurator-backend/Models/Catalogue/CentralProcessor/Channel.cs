﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.CentralProcessor
{
    public class ChannelListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ChannelListItem(Channel channel)
        {
            ID = channel.ID;
            Name = channel.Name;
        }
    }

    public class ChannelDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ChannelDto(Channel channel)
        {
            ID = channel.ID;
            Name = channel.Name;
        }
    }

    public class ChannelDbo
    {
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Channel
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit>? Units { get; set; }
        [JsonIgnore]
        public ICollection<MotherboardUnit>? Motherboards { get; set; }
    }
}
