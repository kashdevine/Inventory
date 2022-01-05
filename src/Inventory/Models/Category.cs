using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Category must have name.")]
        public string? Name { get; set; }

        public ICollection<Inventory>? InventoryCollection { get; set; }
    }
}
