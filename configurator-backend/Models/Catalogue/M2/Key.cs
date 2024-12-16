using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.M2
{
    public class KeyListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public KeyListItem(Key key)
        {
            ID = key.ID;
            Name = key.Name;
        }
    }

    public class KeyParams
    {
        public required ICollection<Key> Keys { get; set; }
    }

    public class KeyDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public KeyDto(Key key)
        {
            ID = key.ID;
            Name = key.Name;
        }
    }

    public class KeyDbo
    {
        public required string Name { get; set; }
        public ICollection<int>? CompatibleKeyIDs { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Key
    {
        public int ID { get; set; }

        public required string Name { get; set; }
        
        public ICollection<Key>? CompatibleKeys { get; set; }


        [JsonIgnore]
        public ICollection<Key>? PhysicalKeys { get; set; }
        [JsonIgnore]
        public ICollection<Slot>? Slots { get; set; }
        [JsonIgnore]
        public ICollection<ExpansionCard>? ExpansionCards { get; set; }
    }
}
