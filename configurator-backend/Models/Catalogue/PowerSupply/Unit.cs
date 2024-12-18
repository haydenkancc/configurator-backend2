using ConfiguratorBackend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.PowerSupply
{
    public class UnitListItem : ComponentListItem
    {
        public string FormFactor { get; set; }
        public string EfficiencyRating { get; set; }
        public string TotalPower { get; set; }
        public string Modularity { get; set; }

        public UnitListItem(Unit unit) : base(unit.Component)
        {
            EfficiencyRating = unit.EfficiencyRating.Name;
            FormFactor = unit.FormFactor.Name;
            Modularity = unit.Modularity.Name;
            TotalPower = $"{unit.TotalPower} W";
        }
    }
    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<ConnectorDtoSimple> Connectors { get; set; }
        public required ICollection<FormFactorDto> FormFactors { get; set; }
        public required ICollection<EfficiencyRatingDto> EfficiencyRatings { get; set; }
        public required ICollection<ModularityDto> Modularities { get; set; }
    }

    public class UnitDto
    {
        public ComponentDto Component { get; set; }
        public FormFactorDto FormFactor { get; set; }
        public EfficiencyRatingDto EfficiencyRating { get; set; }
        public ModularityDto Modularity { get; set; }
        public ICollection<UnitConnectorDto> Connectors { get; set; }
        public int TotalPower { get; set; }
        public int Length { get; set; }
        public bool Fanless { get; set; }

        public UnitDto(ComponentDto component, Unit unit)
        {
            Component = component;
            FormFactor = new FormFactorDto(unit.FormFactor);
            EfficiencyRating = new EfficiencyRatingDto(unit.EfficiencyRating);
            Modularity = new ModularityDto(unit.Modularity);
            Connectors = unit.Connectors.Select(e => new UnitConnectorDto(e)).ToList();

            TotalPower = unit.TotalPower;
            Length = unit.Length;
            Fanless = unit.Fanless;
        }
    }

    public class UnitDbo
    {
        [Required]
        public required ComponentDbo Component { get; set; }
        [Required]
        public required ICollection<UnitConnectorDbo> Connectors { get; set; }
        [Required]
        public required int FormFactorID { get; set; }
        [Required]
        public required int ModularityID { get; set; }
        [Required]
        public required int EfficiencyRatingID { get; set; }
        [Required]
        public required int TotalPower { get; set; }
        [Required]
        public required int Length { get; set; }
        [Required]
        public required bool Fanless { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public required int ComponentID { get; set; }
        public required int FormFactorID { get; set; }
        public required int ModularityID { get; set; }
        public required int EfficiencyRatingID { get; set; }
        public required int TotalPower { get; set; }
        public required int Length { get; set; }
        public required bool Fanless { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public Component Component { get; set; } = null!;
        public FormFactor FormFactor { get; set; } = null!;
        public Modularity Modularity { get; set; } = null!;
        public EfficiencyRating EfficiencyRating { get; set; } = null!;
        public required ICollection<UnitConnector> Connectors { get; set; }
    }
}
