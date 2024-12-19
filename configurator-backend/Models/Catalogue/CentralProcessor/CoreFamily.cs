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
            Name = $"{coreFamily.CodeName} ({coreFamily.AlternateName})";
            Microarchitecture = coreFamily.Microarchitecture.Name;
        }
    }

    public class CoreFamilyParams
    {
        public required ICollection<MicroarchitectureDto> Microarchitectures { get; set; }
    }

    public class CoreFamilyDtoSimple
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public CoreFamilyDtoSimple(CoreFamily coreFamily)
        {
            ID = coreFamily.ID;
            Name = $"{coreFamily.CodeName} ({coreFamily.AlternateName})";
        }
    }

    public class CoreFamilyDto
    {
        public int ID { get; set; }
        public string CodeName { get; set; }
        public string AlternateName { get; set; }
        public MicroarchitectureDto Microarchitecture { get; set; }

        public CoreFamilyDto(CoreFamily coreFamily)
        {
            ID = coreFamily.ID;
            CodeName = coreFamily.CodeName;
            AlternateName = coreFamily.AlternateName;
            Microarchitecture = new MicroarchitectureDto(coreFamily.Microarchitecture);
        }
    }

    public class CoreFamilyDbo
    {
        public required string CodeName { get; set; }
        public required string AlternateName { get; set; }
        public required int MicroarchitectureID { get; set; }
    }

    [Index(nameof(CodeName), IsUnique = true)]
    public class CoreFamily
    {
        public int ID { get; set; }
        public required int MicroarchitectureID { get; set; }
        public required string CodeName { get; set; }
        public required string AlternateName { get; set; }

        public Microarchitecture Microarchitecture { get; set; } = null!;

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
