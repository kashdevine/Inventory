using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Vendor
{
    public class VendorUpdateRequestDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vendor must have name.")]
        public string Name { get; set; }
    }
}
