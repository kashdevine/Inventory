using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Vendor
{
    public class VendorCreateRequestDTO
    {
        [Required(ErrorMessage = "Category name must be provided.")]
        public string? Name { get; set; }
    }
}
