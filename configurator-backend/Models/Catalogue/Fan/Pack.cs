using configurator_backend.Models.Catalogue.General;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Fan
{
    public class PackListItem : ComponentListItem
    {
        public string Size { get; set; }
        public string Rpm { get; set; }
        public string Airflow { get; set; }
        public string NoiseLevel { get; set; }
        public string Pwm { get; set; }

        public PackListItem(Pack pack) : base(pack.Component)
        {
            Name = $"{pack.Component.Manufacturer.Name} {pack.Component.Name} {(pack.Quantity > 1 ? pack.Quantity.ToString() + "-Pack" : "")}";
            Size = $"{pack.Size.SideLength} mm";
            Rpm = $"{pack.Rpm} RPM";
            Airflow = $"{pack.Airflow} CFM";
            NoiseLevel = $"{pack.NoiseLevel} dB";
            Pwm = pack.Pwm ? "Yes" : "No";
        }
    }

    public class PackParams
    {
        public required ComponentParams Component { get; set; }
        public required ICollection<Size> Sizes { get; set; }
        public required ICollection<IO.Connector> Connectors { get; set; }
    }

    public class PackDto
    {
        public ComponentDto Component { get; set; }
        public Size Size { get; set; }
        public int Quantity { get; set; }
        public string Rpm { get; set; }
        public string Airflow { get; set; }
        public string NoiseLevel { get; set; }
        public string StaticPressure { get; set; }
        public bool Pwm { get; set; }

        public ICollection<PackConnector> Connectors { get; set; }

        public PackDto(ComponentDto component, Pack pack)
        {
            Component = component;
            Size = pack.Size;
            Quantity = pack.Quantity;
            Rpm = pack.Rpm;
            Airflow = pack.Airflow;
            NoiseLevel = pack.NoiseLevel;
            StaticPressure = pack.StaticPressure;
            Pwm = pack.Pwm;
            Connectors = pack.Connectors;
        }

    }

    public class PackDbo
    {
        public required ComponentDbo Component { get; set; }
        public int Quantity { get; set; }
        public int SizeID { get; set; }
        public required string Rpm { get; set; }
        public required string Airflow { get; set; }
        public required string NoiseLevel { get; set; }
        public required string StaticPressure { get; set; }
        public bool Pwm { get; set; }

        public required ICollection<PackConnectorDbo> Connectors { get; set; }
    }

    [PrimaryKey(nameof(ComponentID))]
    public class Pack
    {
        public int ComponentID { get; set; }
        public int Quantity { get; set; }
        public int SizeID { get; set; }
        public required string Rpm { get; set; }
        public required string Airflow { get; set; }
        public required string NoiseLevel { get; set; }
        public required string StaticPressure { get; set; }
        public bool Pwm { get; set; }

        [ForeignKey(nameof(ComponentID))]
        public required Component Component { get; set; }
        public required Size Size { get; set; }
        public required ICollection<PackConnector> Connectors { get; set; }
    }
}
