using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Motherboard
{
    public class UnitM2SlotDbo
    {
        [Required]
        public required int SlotID { get; set; }
        [Required]
        public required int SlotPosition { get; set; }
        [Required]
        public required int ConfigurationNumber { get; set; }
        [Required]
        public required ICollection<int> SeriesIDs { get; set; }
        [Required]
        public required ICollection<int> ProcessorIDs { get; set; }
        [Required]
        public required ICollection<int> CoreFamilyIDs { get; set; }
    }

    [PrimaryKey(nameof(UnitID))]
    [Index(nameof(UnitID), nameof(SlotID), nameof(SlotPosition), nameof(ConfigurationNumber), nameof(HasConfigurationNumber), IsUnique = true)]
    public class UnitM2Slot
    {
        public int UnitID { get; set; }
        public required int SlotID { get; set; }

        public required int SlotPosition { get; set; }
        public required int ConfigurationNumber { get; set; }
        public required bool HasConfigurationNumber { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(UnitID))]
        public Unit Unit { get; set; } = null!;
        [JsonIgnore]
        public M2.Slot Slot { get; set; } = null!;

        public ICollection<CentralProcessor.Series> Series { get; set; } = new List<CentralProcessor.Series>();
        public ICollection<CentralProcessor.Unit> Processors { get; set; } = new List<CentralProcessor.Unit>();
        public ICollection<CentralProcessor.CoreFamily> CoreFamilies { get; set; } = new List<CentralProcessor.CoreFamily>();
    }
}
