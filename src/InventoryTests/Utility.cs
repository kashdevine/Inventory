using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTests
{
    public static class Utility
    {
        
        public static void SeedDB(DbContext ctx)
        {
            //Gets List of Brands
            IEnumerable<Brand> brands = SeedBrands();

            // Add all the brands to the Db
            ctx.AddRangeAsync(brands);
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
