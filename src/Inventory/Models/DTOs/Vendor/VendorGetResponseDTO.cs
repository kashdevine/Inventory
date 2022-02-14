using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Vendor
{
    public class VendorGetResponseDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vendor must have name.")]
        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        ICollection<Guid>? ItemIds { get; set; }
    }
}
