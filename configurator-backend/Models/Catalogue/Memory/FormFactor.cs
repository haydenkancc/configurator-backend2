using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Memory
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
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class FormFactor
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Kit> Kits { get; set; } = new List<Kit>();
        [JsonIgnore]
        public ICollection<Motherboard.Unit> Motherboards { get; set; } = new List<Motherboard.Unit>();
    }
}
