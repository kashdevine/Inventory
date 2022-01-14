using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTests
{
    public static class Utility
    {
        
        public static void SeedDB(InventoryContext ctx)
        {
            // Add all the brands to the Db
            ctx.Brands.AddRangeAsync(SeedBrands());
            ctx.SaveChangesAsync();
        }

        public static void ReinitializeDBForTests(InventoryContext ctx)
        {
            ctx.Brands.RemoveRange(ctx.Brands);
            SeedDB(ctx);
        }

        
        public static IEnumerable<Brand> SeedBrands()
        {
            return new List<Brand>() 
            { 
                new Brand(){ Name = "Brand1"},
                new Brand(){ Name = "Brand2"},
                new Brand(){ Name = "Brand3"},
                new Brand(){ Name = "Brand4"},
            };
        }
    }
}
