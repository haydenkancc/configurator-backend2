using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Case
{
    public class StorageAreaDto
    {
        public ICollection<DriveBayDto> DriveBays { get; set; }
        
        public StorageAreaDto(StorageArea area)
        {
            DriveBays = area.DriveBays.Select(e => new DriveBayDto(e)).ToList();
        }
    }
    public class StorageAreaDbo
    {
        [Required]
        public required ICollection<DriveBayDbo> DriveBays { get; set; }
    }

    public class StorageArea
    {
        public int ID { get; set; }
        public int LayoutID { get; set; }

        public required ICollection<DriveBay> DriveBays { get; set; }

        [JsonIgnore]
        public Layout Layout { get; set; } = null!;
    }
}
