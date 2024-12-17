using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.CentralProcessor
{
    public class CoreFamilyListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Microarchitecture { get; set; }

        public CoreFamilyListItem(CoreFamily coreFamily)
        {
            ID = coreFamily.ID;
            Name = coreFamily.Name;
            Microarchitecture = coreFamily.Microarchitecture.Name;
        }
    }

    public class CoreFamilyDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Microarchitecture Microarchitecture { get; set; }

        public CoreFamilyDto(CoreFamily coreFamily)
        {
            ID = coreFamily.ID;
            Name = coreFamily.Name;
            Microarchitecture = coreFamily.Microarchitecture;
        }
    }

    public class CoreFamilyDbo
    {
        public required string Name { get; set; }
        public required int MicroarchitectureID { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class CoreFamily
    {
        public int ID { get; set; }
        public required int MicroarchitectureID { get; set; }
        public required string Name { get; set; }

        public Microarchitecture Microarchitecture { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
