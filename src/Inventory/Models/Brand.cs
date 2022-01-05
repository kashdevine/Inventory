using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Brand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Brand name must be provided.")]
        public string? Name { get; set; }

        public ICollection<Inventory>? InventoryCollection { get; set; }
    }
}
