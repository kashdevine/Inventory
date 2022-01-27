﻿using System;
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
        public const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=TestInventoryDb;Trusted_Connection=True;MultipleActiveResultSets=true";


        public static async Task SeedDB(InventoryContext ctx)
        {
            // Add all the samples to the Db
            await ctx.Brands!.AddRangeAsync(SeedBrands());
            await ctx.Categories!.AddRangeAsync(SeedCategories());
            await ctx.Vendors!.AddRangeAsync(SeedVendors());
            await ctx.SaveChangesAsync();
        }

        public static async Task ReinitializeDBForTests(InventoryContext ctx)
        {
            if (! ctx.Database.CanConnect())
            {
                await ctx.Database.EnsureCreatedAsync();
            }

            ctx.Brands.RemoveRange(ctx.Brands);
            ctx.Categories.RemoveRange(ctx.Categories);
            ctx.Vendors.RemoveRange(ctx.Vendors);
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

        public static IEnumerable<Category> SeedCategories()
        {
            return new List<Category>()
            {
                new Category(){ Name = "Category1"},
                new Category(){ Name = "Category2"},
                new Category(){ Name = "Category3"},
                new Category(){ Name = "Category4"},
            };
        }

        public static IEnumerable<Vendor> SeedVendors()
        {
            return new List<Vendor>()
            {
                new Vendor(){ Name = "Vendor1"},
                new Vendor(){ Name = "Vendor2"},
                new Vendor(){ Name = "Vendor3"},
                new Vendor(){ Name = "Vendor4"},
            };
        }

        public static async Task<Brand> getBrandForId(InventoryContext ctx)
        {
            await SeedDB(ctx);

            return await ctx.Brands.FirstOrDefaultAsync(b => b.Name == "Brand1");
        }
        public static async Task<Category> GetCategoryForId(InventoryContext ctx)
        {
            await SeedDB(ctx);
            return await ctx.Categories.FirstOrDefaultAsync(c => c.Name == "Category1");
        }


        public static async Task<Vendor> GetVendorForId(InventoryContext ctx)
        {
            await SeedDB(ctx);
            return await ctx.Vendors.FirstOrDefaultAsync(c => c.Name == "Vendor1");
        }
    }
}
