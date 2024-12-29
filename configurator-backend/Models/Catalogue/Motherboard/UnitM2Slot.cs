using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Motherboard
{
    public class UnitM2SlotDto
    {
        public M2.SlotDtoSimple Slot { get; set; }
        public int SlotPosition { get; set; }
        public int ConfigurationNumber { get; set; }
        public ICollection<CentralProcessor.SeriesDto> Series { get; set; }
        public ICollection<CentralProcessor.UnitDtoSimple> Processors { get; set; }
        public ICollection<CentralProcessor.CoreFamilyDtoSimple> CoreFamilies { get; set; }

        public UnitM2SlotDto(UnitM2Slot slot)
        {
            Slot = new M2.SlotDtoSimple(slot.Slot);
            SlotPosition = slot.SlotPosition;
            ConfigurationNumber = slot.ConfigurationNumber;
            Series = slot.Series.Select(e => new CentralProcessor.SeriesDto(e)).ToList();
            Processors = slot.Processors.Select(e => new CentralProcessor.UnitDtoSimple(e)).ToList();
            CoreFamilies = slot.CoreFamilies.Select(e => new CentralProcessor.CoreFamilyDtoSimple(e)).ToList();
        }
    }

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

    [Index(nameof(UnitID), nameof(SlotID), nameof(SlotPosition), nameof(ConfigurationNumber), nameof(HasConfigurationNumber), IsUnique = true)]
    public class UnitM2Slot
    {
        public int ID { get; set; }
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
