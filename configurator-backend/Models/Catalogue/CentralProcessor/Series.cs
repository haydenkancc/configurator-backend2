using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.CentralProcessor
{
    public class SeriesListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SeriesListItem(Series series)
        {
            ID = series.ID;
            Name = series.Name;
        }
    }

    public class SeriesDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SeriesDto(Series series)
        {
            ID = series.ID;
            Name = series.Name;
        }
    }

    public class SeriesDbo
    {
        [Required]
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Series
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
    }
}
