using Inventory.Models;

namespace Inventory.Contracts
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetCategories();

        public Task<Category> GetCategoryById(Guid Id);

        public Task<Category> GetCategoryByName(string Name);

        public Task<Category> CreateCategory(Category Category);

        public Task<bool> CategoryDoesExist(string CategoryName);

        public Task<Category> UpdateCategory(Category Category);

        public Task<bool> DeleteCategory(Guid Id);

        public Task<bool> Save();
    }
}
