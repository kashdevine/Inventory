using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Item
{
    public class ItemUpdateRequestDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Inventory Name Required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description Required.")]
        public string? Description { get; set; }

        [Display(Name = "Per Unit Cost")]
        [Required(ErrorMessage = "Per Unit Cost Needed for Item.")]
        public decimal PerUnitCost { get; set; }

        public string? SKU { get; set; }

        public string? ExternalSKU { get; set; }

        public string? Barcode { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price Needed for Item.")]
        public decimal Price { get; set; }

        public int Variant { get; set; }

        [Display(Name = "Is Digital")]
        public bool IsDigital { get; set; }

        [Display(Name = "Available Stock")]
        public int? AvailableStock { get; set; }

        [Display(Name = "Restock Threshold")]
        public int? RestockThreshold { get; set; }

        [Display(Name = "Maximum Stock Threshold")]
        public int? MaxStockThreshold { get; set; }

        public Guid? BrandId { get; set; }

        public Guid? CategoryId { get; set; }

        public Guid? VendorId { get; set; }
    }
}
