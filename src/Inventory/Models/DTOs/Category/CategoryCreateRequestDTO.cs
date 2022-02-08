using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Category
{
    public class CategoryCreateRequestDTO
    {
        [Required(ErrorMessage = "Category name must be provided.")]
        public string? Name { get; set; }
    }
}
