using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.CentralProcessor
{
    public class MicroarchitectureListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MicroarchitectureListItem(Microarchitecture microarchitecture)
        {
            ID = microarchitecture.ID;
            Name = microarchitecture.Name;
        }
    }

    public class MicroarchitectureDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public MicroarchitectureDto(Microarchitecture microarchitecture)
        {
            ID = microarchitecture.ID;
            Name = microarchitecture.Name;
        }
    }

    public class MicroarchitectureDbo
    {
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Microarchitecture
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit>? Units { get; set; }
        [JsonIgnore]
        public ICollection<CoreFamily>? CoreFamilies { get; set; }
    }
}
