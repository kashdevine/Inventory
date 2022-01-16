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
        
        public static async Task SeedDB(InventoryContext ctx)
        {
            // Add all the brands to the Db
           await ctx.Brands.AddRangeAsync(SeedBrands());
           await ctx.SaveChangesAsync();
        }

        public static async Task ReinitializeDBForTests(InventoryContext ctx)
        {
            ctx.Brands.RemoveRange(ctx.Brands);
            await SeedDB(ctx);
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

        public static async Task<Brand> getBrandForId(InventoryContext ctx)
        {
            await SeedDB(ctx);

            return ctx.Brands.FirstOrDefault(b => b.Name == "Brand1");
        }
    }
}
