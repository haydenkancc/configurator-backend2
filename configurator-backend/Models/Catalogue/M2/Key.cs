using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.M2
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
        public ICollection<Key> CompatibleKeys { get; set; }

        public KeyDto(Key key)
        {
            ID = key.ID;
            Name = key.Name;
            CompatibleKeys = key.CompatibleKeys;
        }
    }

    public class KeyDbo
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required ICollection<int> CompatibleKeyIDs { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Key
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        public ICollection<Key> CompatibleKeys { get; set; } = new List<Key>();


        [JsonIgnore]
        public ICollection<Key> PhysicalKeys { get; set; } = new List<Key>();
        [JsonIgnore]
        public ICollection<Slot> Slots { get; set; } = new List<Slot>();
        [JsonIgnore]
        public ICollection<ExpansionCard> ExpansionCards { get; set; } = new List<ExpansionCard>();
    }
}
