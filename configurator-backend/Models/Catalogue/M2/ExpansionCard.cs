using configurator_backend.Models.Catalogue.General;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.M2
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
        public int KeyID { get; set; }
        public int FormFactorID { get; set; }
        public int VersionID { get; set; }
        public int LaneSizeID { get; set; }
    }

    public class ExpansionCard
    {
        public int ID { get; set; }
        public int KeyID { get; set; }
        public int FormFactorID { get; set; }
        public int VersionID { get; set; }
        public int LaneSizeID { get; set; }

        public required Key Key { get; set; }
        public required FormFactor FormFactor { get; set; }
        public required Pcie.Version Version { get; set; }
        public required Pcie.Size LaneSize { get; set; }
    }
}
