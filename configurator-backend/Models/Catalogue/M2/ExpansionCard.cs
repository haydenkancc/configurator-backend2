using ConfiguratorBackend.Models.Catalogue.General;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.M2
{
    public class ExpansionCardParams
    {
        public required ICollection<Key> Keys { get; set; }
        public required ICollection<FormFactor> FormFactors { get; set; }
        public required ICollection<Pcie.Size> LaneSizes { get; set; }
        public required ICollection<Pcie.Version> Versions { get; set; }
    }

    public class ExpansionCardDto
    {
        public Key Key { get; set; }
        public FormFactor FormFactor { get; set; }
        public Pcie.Version Version { get; set; }
        public Pcie.Size LaneSize { get; set; }

        public ExpansionCardDto(ExpansionCard expansionCard)
        {
            Key = expansionCard.Key;
            FormFactor = expansionCard.FormFactor;
            Version = expansionCard.Version;
            LaneSize = expansionCard.LaneSize;
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
