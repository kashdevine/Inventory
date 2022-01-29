using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Models
{
    public class Item
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Inventory Name Required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description Required.")]
        public string? Description { get; set; }

        [Display(Name ="Per Unit Cost")]
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

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }



        public Guid? BrandId { get; set; }

        public Brand? Brand { get; set; }

        public Guid? CategoryId { get; set; }

        public Category? Category { get; set; }

        public Guid? VendorId { get; set; }

        public Vendor? Vendor { get; set; }



        private decimal netProfit;

        public decimal NetProfit
        {
            get { return netProfit; }
            private set { netProfit = this.PerUnitCost - this.Price; }
        }


        private bool needsReorder;

        public bool NeedsReorder
        {
            get { return needsReorder; }
            private set {
                if((this.AvailableStock != null && this.RestockThreshold != null) 
                    && (this.AvailableStock > 0 && this.RestockThreshold > 0))
                {
                    needsReorder = this.AvailableStock <= this.RestockThreshold;
                }
            }

        }



    }
}
