using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services
{
    public class BrandRepository : IBrandRepository
    {
        private readonly InventoryContext _ctx;

        public BrandRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Brand> CreateBrand(Brand brand)
        {
            if (await BrandDoesExist(brand.Name))
            {
                return await _ctx.Brands.FirstOrDefaultAsync(b => b.Name == brand.Name);
            }

            await _ctx.Brands.AddAsync(brand);

            if (await Save())
            {
                return await _ctx.Brands.FirstOrDefaultAsync(b => b.Id == brand.Id);
            }

            return null;
        }

        public async Task<bool> DeleteBrand(Guid Id)
        {
            if (!await BrandDoesExist(Id))
            {
                return await Save();
            }
            var deletedBrand = await GetBrandById(Id);

            _ctx.Brands?.Remove(deletedBrand);

            return await Save();
        }

        public async Task<Brand> GetBrandById(Guid Id)
        {
            return await _ctx.Brands.FindAsync(Id);
        }

        public async Task<Brand> GetBrandByName(string Name)
        {
            Brand brand = await _ctx.Brands.FirstOrDefaultAsync(b => b.Name == Name);
            return brand;
        }

        public async Task<IEnumerable<Brand>> GetBrands()
        {
            return _ctx.Brands.Where(b => b.Name != null);
        }

        public async Task<bool> Save()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<Brand> UpdateBrand(Brand brand)
        {
            if (!await BrandDoesExist(brand.Id))
            {
                return null;
            }
            _ctx.Brands.Update(brand);
            await Save();
            return brand;
        }

        public async Task<bool> BrandDoesExist(string brandName)
        {
            var existing = await GetBrandByName(brandName);

            return existing != null;
        }

        public async Task<bool> BrandDoesExist(Guid brandId)
        {
            var existing = await GetBrandById(brandId);

            return existing != null;
        }

    }
}
