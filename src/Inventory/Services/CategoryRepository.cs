using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;

namespace Inventory.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryContext _ctx;
        public CategoryRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }
        public Task<bool> CategoryDoesExist(string CategoryName)
        {
            throw new NotImplementedException();
        }

        public Task<Category> CreateCategory(Category Category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategory(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(Category Category)
        {
            throw new NotImplementedException();
        }
    }
}
