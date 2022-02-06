using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Brand
{
    public class BrandUpdateRequestDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Brand name must be provided.")]
        public string? Name { get; set; }
    }
}
