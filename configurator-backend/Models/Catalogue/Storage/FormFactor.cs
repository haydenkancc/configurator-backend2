using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Storage
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
        public ICollection<CaseUnit> Units { get; set; } = new List<CaseUnit>();
    }
}
