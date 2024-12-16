using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.M2
{
    public class SlotListItemSimple
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public SlotListItemSimple(Slot slot)
        {
            ID = slot.ID;
            Name = $"{string.Join('/', slot.FormFactors.Select(formFactor => formFactor.Name))} {slot.Key.Name}-key {slot.Version.Name} x{slot.LaneSize.LaneCount}";
        }
    }



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
        public required ICollection<Key> Keys { get; set; }
        public required ICollection<FormFactor> FormFactors { get; set; }
        public required ICollection<Pcie.Version> Versions { get; set; }
        public required ICollection<Pcie.Size> LaneSizes { get; set; }
    }
    public class SlotDto
    {
        public int ID { get; set; }
        public Key Key { get; set; }
        public Pcie.Size LaneSize { get; set; }
        public Pcie.Version Version { get; set; }
        public ICollection<FormFactor> FormFactors { get; set; }

        public SlotDto(Slot slot)
        {
            ID = slot.ID;
            Key = slot.Key;
            FormFactors = slot.FormFactors;
            Version = slot.Version;
            LaneSize = slot.LaneSize;
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
        public int KeyID { get; set; }
        public int LaneSizeID { get; set; }
        public int VersionID { get; set; }

        public required Key Key { get; set; }
        public required Pcie.Size LaneSize { get; set; }
        public required Pcie.Version Version { get; set; }
        public required ICollection<FormFactor> FormFactors { get; set; }

        [JsonIgnore]
        public ICollection<MotherboardUnitM2Slot>? Motherboards { get; set; }
    }
}
