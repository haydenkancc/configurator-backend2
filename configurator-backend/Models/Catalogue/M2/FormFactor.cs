using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.M2
{
    public class FormFactorListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public FormFactorListItem(FormFactor formFactor)
        {
            ID = formFactor.ID;
            Name = formFactor.Name;
        }
    }

    public class FormFactorDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public FormFactorDto(FormFactor formFactor)
        {
            ID = formFactor.ID;
            Name = formFactor.Name;
        }
    }

    public class FormFactorDbo
    {
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class FormFactor
    {
        public int ID { get; set; }

        public required string Name { get; set; }


        [JsonIgnore]
        public ICollection<Slot>? Slots { get; set; }
        [JsonIgnore]
        public ICollection<ExpansionCard>? ExpansionCards { get; set; }
    }
}
