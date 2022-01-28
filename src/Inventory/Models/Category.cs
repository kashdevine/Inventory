using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Category must have name.")]
        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        ICollection<Guid>? ItemIds { get; set; }
        public Item? Item { get; set; }
    }
}
