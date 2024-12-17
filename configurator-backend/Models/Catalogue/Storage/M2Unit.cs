using ConfiguratorBackend.Models.Catalogue.General;
using ConfiguratorBackend.Models.Catalogue;
using System.ComponentModel.DataAnnotations;

namespace ConfiguratorBackend.Models.Catalogue.Storage
{
    public class M2UnitDto : BaseUnitDto
    {
        public M2.ExpansionCardDto ExpansionCard { get; set; }

        public M2UnitDto(ComponentDto component, DriveDto drive, M2.ExpansionCardDto expansionCard, M2Unit unit) : base(component, drive, unit)
        {
            ExpansionCard = expansionCard;
        }
    }

    public class M2UnitDbo : BaseUnitDbo
    {
        [Required]
        public required M2.ExpansionCardDbo ExpansionCard { get; set; }
    }

    public class M2Unit : Unit
    {
        public int ExpansionCardID { get; set; }

        public M2.ExpansionCard ExpansionCard { get; set; } = null!;
    }
}
