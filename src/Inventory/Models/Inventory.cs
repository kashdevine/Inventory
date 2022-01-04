using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Inventory
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage ="Inventory Name Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description Required")]
        public string? Description { get; set; }

        public string? SKU { get; set; }

        public string? ExternalSKU { get; set; }
        
        public string? Barcode { get; set; }

        public string? Brand { get; set; }

        public string? Vendor { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Quantity { get; set; }

        [Display(Name ="Per Unit Cost")]
        public int PerUnitCost { get; set; }

        public int Price { get; set; }

        public int Variant { get; set; }

    }
}
