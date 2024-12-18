using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace ConfiguratorBackend.Models.Catalogue.M2
{
    public class SlotListItem
    {
        public int ID { get; set; }
        public string KeyName { get; set; }
        public string LaneSize { get; set; }
        public string Version { get; set; }
        public string FormFactors { get; set; }

        public SlotListItem(Slot slot)
        {
            ID = slot.ID;
            KeyName = slot.Key.Name;
            FormFactors = string.Join('/', slot.FormFactors.Select(formFactor => formFactor.Name));
            Version = slot.Version.Name;
            LaneSize = $"x{slot.LaneSize.LaneCount}";
        }
    }

    public class SlotParams
    {
        public required ICollection<KeyDtoSimple> Keys { get; set; }
        public required ICollection<FormFactorDto> FormFactors { get; set; }
        public required ICollection<Pcie.VersionDto> Versions { get; set; }
        public required ICollection<Pcie.SizeDto> LaneSizes { get; set; }
    }

    public class SlotDtoSimple
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SlotDtoSimple(Slot slot)
        {
            ID = slot.ID;
            Name = $"{string.Join('/', slot.FormFactors.Select(formFactor => formFactor.Name))} {slot.Key.Name}-key {slot.Version.Name} x{slot.LaneSize.LaneCount}";
        }
    }

    public class SlotDto
    {
        public int ID { get; set; }
        public KeyDto Key { get; set; }
        public Pcie.SizeDto LaneSize { get; set; }
        public Pcie.VersionDto Version { get; set; }
        public ICollection<FormFactorDto> FormFactors { get; set; }

        public SlotDto(Slot slot)
        {
            ID = slot.ID;
            Key = new KeyDto(slot.Key);
            Version = new Pcie.VersionDto(slot.Version);
            LaneSize = new Pcie.SizeDto(slot.LaneSize);
            FormFactors = slot.FormFactors.Select(e => new FormFactorDto(e)).ToList();
        }
    }

    public class SlotDbo
    {
        public int KeyID { get; set; }
        public int LaneSizeID { get; set; }
        public int VersionID { get; set; }
        public required List<int> FormFactorIDs { get; set; }
    }

    public class Slot
    {
        [SwaggerSchema(ReadOnly = true)]
        public int ID { get; set; }
        public required int KeyID { get; set; }
        public required int LaneSizeID { get; set; }
        public required int VersionID { get; set; }

        public Key Key { get; set; } = null!;
        public Pcie.Size LaneSize { get; set; } = null!;
        public Pcie.Version Version { get; set; } = null!;
        public required ICollection<FormFactor> FormFactors { get; set; }

        [JsonIgnore]
        public ICollection<Motherboard.UnitM2Slot> Motherboards { get; set; } = new List<Motherboard.UnitM2Slot>();
    }
}
