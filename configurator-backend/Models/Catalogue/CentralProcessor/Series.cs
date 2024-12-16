using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.CentralProcessor
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
        public required string Name { get; set; }
    }

    [Index(nameof(Name), IsUnique = true)]
    public class Series
    {
        public int ID { get; set; }

        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Unit>? Units { get; set; }
    }
}
