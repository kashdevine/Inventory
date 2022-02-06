using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Brand
{
    public class BrandCreateRequestDTO
    {
        [Required(ErrorMessage = "Brand name must be provided.")]
        public string? Name { get; set; }
    }
}
