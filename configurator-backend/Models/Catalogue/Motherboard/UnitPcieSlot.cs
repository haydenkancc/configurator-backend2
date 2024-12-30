using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.Motherboard
{
    public class UnitPcieSlotDto
    {
        public Pcie.SlotDtoSimple Slot { get; set; }
        public int SlotPosition { get; set; }
        public int ConfigurationNumber { get; set; }
        public ICollection<CentralProcessor.SeriesDto> Series { get; set; }
        public ICollection<CentralProcessor.UnitDtoSimple> Processors { get; set; }
        public ICollection<CentralProcessor.CoreFamilyDtoSimple> CoreFamilies { get; set; }

        public UnitPcieSlotDto(UnitPcieSlot slot)
        {
            Slot = new Pcie.SlotDtoSimple(slot.Slot);
            SlotPosition = slot.SlotPosition;
            ConfigurationNumber = slot.ConfigurationNumber;
            Series = slot.Series.Select(e => new CentralProcessor.SeriesDto(e)).ToList();
            Processors = slot.Processors.Select(e => new CentralProcessor.UnitDtoSimple(e)).ToList();
            CoreFamilies = slot.CoreFamilies.Select(e => new CentralProcessor.CoreFamilyDtoSimple(e)).ToList();
        }
    }

    public class UnitPcieSlotDbo
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
    public class UnitPcieSlot
    {
        public int ID { get; set; }
        public int UnitID { get; set; }
        public required int SlotID { get; set; }

        public required int SlotPosition { get; set; }
        public required int ConfigurationNumber { get; set; }
        public required bool HasConfigurationNumber { get; set; }

        [JsonIgnore]
        public Unit Unit { get; set; } = null!;
        [JsonIgnore]
        public Pcie.Slot Slot { get; set; } = null!;

        public ICollection<CentralProcessor.Series> Series { get; set; } = new List<CentralProcessor.Series>();
        public ICollection<CentralProcessor.Unit> Processors { get; set; } = new List<CentralProcessor.Unit>();
        public ICollection<CentralProcessor.CoreFamily> CoreFamilies { get; set; } = new List<CentralProcessor.CoreFamily>();
    }
}
