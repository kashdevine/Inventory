using System.ComponentModel.DataAnnotations;


namespace Inventory.Models
{
    public class Brand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Brand name must be provided.")]
        public string? Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdated { get; set; }

        ICollection<Guid>? ItemIds { get; set; }
        public Item? Item { get; set; }
    }
}
