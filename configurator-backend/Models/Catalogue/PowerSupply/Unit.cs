using configurator_backend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.PowerSupply
{
    public class UnitParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<Connector> Connectors { get; set; }
        public required ICollection<FormFactor> FormFactors { get; set; }
        public required ICollection<EfficiencyRating> EfficiencyRatings { get; set; }
        public required ICollection<Modularity> Modularities { get; set; }
    }

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

    public class UnitDto
    {
        public ComponentDto Component { get; set; }
        public FormFactor FormFactor { get; set; }
        public EfficiencyRating EfficiencyRating { get; set; }
        public Modularity Modularity { get; set; }
        public ICollection<UnitConnector> Connectors { get; set; }
        public int TotalPower { get; set; }
        public int Length { get; set; }
        public bool Fanless { get; set; }

        public UnitDto(ComponentDto component, Unit unit)
        {
            Component = component;
            FormFactor = unit.FormFactor;
            EfficiencyRating = unit.EfficiencyRating;
            Modularity = unit.Modularity;
            Connectors = unit.Connectors;

            TotalPower = unit.TotalPower;
            Length = unit.Length;
            Fanless = unit.Fanless;
        }
    }

    public class UnitDbo
    {
        public required ComponentDbo Component { get; set; }
        public required ICollection<UnitConnectorDbo> Connectors { get; set; }
        public int FormFactorID { get; set; }
        public int ModularityID { get; set; }
        public int EfficiencyRatingID { get; set; }
        public int TotalPower { get; set; }
        public int Length { get; set; }
        public bool Fanless { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Unit
    {
        public int ComponentID { get; set; }
        public int FormFactorID { get; set; }
        public int ModularityID { get; set; }
        public int EfficiencyRatingID { get; set; }
        public int TotalPower { get; set; }
        public int Length { get; set; }
        public bool Fanless { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public required Component Component { get; set; }
        public required FormFactor FormFactor { get; set; }
        public required Modularity Modularity { get; set; }
        public required EfficiencyRating EfficiencyRating { get; set; }
        public required ICollection<UnitConnector> Connectors { get; set; }
    }
}
