using ConfiguratorBackend.Models.Catalogue.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.M2
{
    public class ExpansionCardParams
    {
        public required ICollection<KeyDtoSimple> Keys { get; set; }
        public required ICollection<FormFactorDto> FormFactors { get; set; }
        public required ICollection<Pcie.SizeDto> LaneSizes { get; set; }
        public required ICollection<Pcie.VersionDto> Versions { get; set; }
    }

    public class ExpansionCardDto
    {
        public KeyDtoSimple Key { get; set; }
        public FormFactorDto FormFactor { get; set; }
        public Pcie.VersionDto Version { get; set; }
        public Pcie.SizeDto LaneSize { get; set; }

        public ExpansionCardDto(ExpansionCard expansionCard)
        {
            Key = new KeyDtoSimple(expansionCard.Key);
            FormFactor = new FormFactorDto(expansionCard.FormFactor);
            Version = new Pcie.VersionDto(expansionCard.Version);
            LaneSize = new Pcie.SizeDto(expansionCard.LaneSize);
        }
    }

    public class ExpansionCardDbo
    {
        [Required]
        public required int KeyID { get; set; }
        [Required]
        public required int FormFactorID { get; set; }
        [Required]
        public required int VersionID { get; set; }
        [Required]
        public required int LaneSizeID { get; set; }
    }

    public class ExpansionCard
    {
        public int ID { get; set; }
        public required int KeyID { get; set; }
        public required int FormFactorID { get; set; }
        public required int VersionID { get; set; }
        public required int LaneSizeID { get; set; }

        public Key Key { get; set; } = null!;
        public FormFactor FormFactor { get; set; } = null!;
        public Pcie.Version Version { get; set; } = null!;
        public Pcie.Size LaneSize { get; set; } = null!;
    }
}
