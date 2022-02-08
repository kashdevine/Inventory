using System.ComponentModel.DataAnnotations;

namespace Inventory.Models.DTOs.Category
{
    public class CategoryGetResponeDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category must have name.")]
        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        ICollection<Guid>? ItemIds { get; set; }
    }
}
