using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Vendor must have name.")]
        public string? Name { get; set; }

        public ICollection<Inventory>? InventoryCollection { get; set; }
    }
}
