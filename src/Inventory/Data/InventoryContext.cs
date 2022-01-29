using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }


        public DbSet<Brand>? Brands { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Vendor>? Vendors { get; set; }

        public DbSet<Item>? Items { get; set; }
    }
}
