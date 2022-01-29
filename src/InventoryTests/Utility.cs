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
        public const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=TestInventoryDb;Trusted_Connection=True;MultipleActiveResultSets=true";


        public static async Task SeedDB(InventoryContext ctx)
        {
            // Add all the samples to the Db
            await ctx.Brands!.AddRangeAsync(SeedBrands());
            await ctx.Categories!.AddRangeAsync(SeedCategories());
            await ctx.Vendors!.AddRangeAsync(SeedVendors());
            await ctx.Items!.AddRangeAsync(SeedItems());
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
            ctx.Items.RemoveRange(ctx.Items);
            await SeedDB(ctx);
        }

        
        public static IEnumerable<Brand> SeedBrands()
        {
            return new List<Brand>() 
            { 
                new Brand(){ Name = "Brand1"},
                new Brand(){ Name = "Brand2"},
                new Brand(){ Name = "Brand3"},
                new Brand(){ Name = "Brand4"}
            };
        }

        public static IEnumerable<Category> SeedCategories()
        {
            return new List<Category>()
            {
                new Category(){ Name = "Category1"},
                new Category(){ Name = "Category2"},
                new Category(){ Name = "Category3"},
                new Category(){ Name = "Category4"}
            };
        }

        public static IEnumerable<Vendor> SeedVendors()
        {
            return new List<Vendor>()
            {
                new Vendor(){ Name = "Vendor1"},
                new Vendor(){ Name = "Vendor2"},
                new Vendor(){ Name = "Vendor3"},
                new Vendor(){ Name = "Vendor4"}
            };
        }
        public static IEnumerable<Item> SeedItems()
        {

            return new List<Item>()
            {
                new Item(){ Name = "Item1", Description="Item1 Description", PerUnitCost = 0, Price = 100, IsDigital = true},
                new Item(){ Name = "Item2", Description="Item2 Description", PerUnitCost = 20, Price = 100, IsDigital = true},
                new Item(){ Name = "Item3", Description="Item3 Description", PerUnitCost = 20, Price = 100,
                            IsDigital = false, AvailableStock = 10, RestockThreshold = 3,
                            Length = 10, Height = 10, Width = 10,
                            Weight = 300},
                new Item(){ Name = "Item4", Description="Item4 Description", PerUnitCost = 20, Price = 100,
                            IsDigital = false, AvailableStock = 5, RestockThreshold = 5,
                            Length = 10, Height = 10, Width = 10,
                            Weight = 300}
            };
        }

        public static async Task<Brand> getBrandForId(InventoryContext ctx, string BrandName = "Brand1")
        {
            await SeedDB(ctx);

            return await ctx.Brands.FirstOrDefaultAsync(b => b.Name == BrandName);
        }
        public static async Task<Category> GetCategoryForId(InventoryContext ctx, string CategoryName = "Category1")
        {
            await SeedDB(ctx);
            return await ctx.Categories.FirstOrDefaultAsync(c => c.Name == CategoryName);
        }


        public static async Task<Vendor> GetVendorForId(InventoryContext ctx, string VendorName = "Vendor1")
        {
            await SeedDB(ctx);
            return await ctx.Vendors.FirstOrDefaultAsync(c => c.Name == VendorName);
        }

        public static async Task<Item> GetItemForId(InventoryContext ctx)
        {
            await SeedDB(ctx);
            return await ctx.Items.FirstOrDefaultAsync(c => c.Name == "Item1");
        }
    }
}
