using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;

namespace Inventory.Services
{
    public class BrandRepository : IBrandRepository
    {
        private readonly InventoryContext _ctx;

        public BrandRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Brand> CreateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBrand(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Brand> GetBrandById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Brand> GetBrandByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Brand>> GetBrands()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

    }
}
