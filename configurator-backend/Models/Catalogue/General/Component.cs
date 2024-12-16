using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace configurator_backend.Models.Catalogue.General
{
    public class ComponentListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        public ComponentListItem(Component component)
        {
            ID = component.ID;
            Price = component.Saleable ? (component.OnSale ? component.SalePrice : component.RegularPrice).ToString("C") : "N/A";
            Name = $"{component.Manufacturer.Name} {component.Name}";
        }
    }

    public class ComponentParams
    {
        public required ICollection<Manufacturer> Manufacturers { get; set; }
    }

    public class ComponentDto
    {
        public int ID { get; set; }
        public string SKU { get; set; }
        public string PartNumber { get; set; }
        public string Name { get; set; }
        public decimal RegularPrice { get; set; }
        public decimal SalePrice { get; set; } = 0m;
        public bool OnSale { get; set; }
        public bool Saleable { get; set; }
        public Manufacturer Manufacturer { get; set; }

        public ComponentDto(Component component)
        {
            Manufacturer = component.Manufacturer;
            ID = component.ID;
            Name = component.Name;
            PartNumber = component.PartNumber;
            RegularPrice = component.RegularPrice;
            SalePrice = component.SalePrice;
            OnSale = component.OnSale;
            SKU = component.SKU;
            Saleable = component.Saleable;
        }
    }

    public class ComponentDbo
    {
        public int ManufacturerID { get; set; }
        public required string SKU { get; set; }
        public required string PartNumber { get; set; }
        public required string Name { get; set; }
        public decimal RegularPrice { get; set; } = 0m;
        public decimal SalePrice { get; set; } = 0m;
        public bool OnSale { get; set; }
        public bool Saleable { get; set; }
    }


    public class Component
    {
        public int ID { get; set; }
        public int ManufacturerID { get; set; }

        [MinLength(10)]
        [StringLength(10)]
        public required string SKU { get; set; }
        public required string PartNumber { get; set; }
        public required string Name { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal RegularPrice { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal SalePrice { get; set; }
        public bool OnSale { get; set; }
        public bool Saleable { get; set; }
        public required Manufacturer Manufacturer { get; set; }
    }
}
