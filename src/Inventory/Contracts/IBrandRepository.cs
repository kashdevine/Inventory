using Inventory.Models;

namespace Inventory.Contracts
{
    public interface IBrandRepository
    {
        public Task<IEnumerable<Brand>> GetBrands();

        public Task<Brand> GetBrandById(Guid Id);

        public Task<Brand> GetBrandByName(string Name);

        public Task<Brand> CreateBrand(Brand brand);

        public Task<bool> UpdateBrand(Brand brand);

        public Task<bool> DeleteBrand(Guid Id);

        public Task<bool> Save();

    }
}
