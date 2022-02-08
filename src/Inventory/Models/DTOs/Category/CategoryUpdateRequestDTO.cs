using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Category
{
    public class CategoryUpdateRequestDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category must have name.")]
        public string Name { get; set; }
    }
}
