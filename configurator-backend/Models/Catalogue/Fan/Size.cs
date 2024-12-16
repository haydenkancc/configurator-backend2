using configurator_backend.Models.Catalogue.CentralProcessor;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace configurator_backend.Models.Catalogue.Fan
{
    public class SizeListItem
    {
        public int ID { get; set; }
        public string SideLength { get; set; }

        public SizeListItem(Size size)
        {
            ID = size.ID;
            SideLength = $"{size.SideLength} mm";
        }
    }

    public class SizeDto
    {
        public int ID { get; set; }
        public int SideLength { get; set; }

        public SizeDto(Size size)
        {
            ID = size.ID;
            SideLength = size.SideLength;
        }
    }

    public class SizeDbo
    {
        public int SideLength { get; set; }
    }

    [Index(nameof(SideLength), IsUnique = true)]
    public class Size
    {
        public int ID { get; set; }

        public int SideLength { get; set; }

        [JsonIgnore]
        public ICollection<Unit>? Units { get; set; }
    }
}
