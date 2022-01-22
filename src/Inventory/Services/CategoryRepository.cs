using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryContext _ctx;

        public CategoryRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> CategoryDoesExist(string CategoryName)
        {
            var existing = await _ctx.Categories.FirstOrDefaultAsync(c => c.Name == CategoryName);
            return existing != null;
        }

        public async Task<bool> CategoryDoesExist(Guid CategoryId)
        {
            var existing = await _ctx.Categories.FirstOrDefaultAsync(c => c.Id == CategoryId);
            return existing != null;
        }

        public async Task<Category> CreateCategory(Category Category)
        {
            if (await CategoryDoesExist(Category.Name))
            {
                return await _ctx.Categories.FirstOrDefaultAsync(c => c.Name == Category.Name);
            }

            await _ctx.Categories.AddAsync(Category);

            if (await Save())
            {
                return Category;
            }
            return null;
        }

        public async Task<bool> DeleteCategory(Guid Id)
        {
            if (!await CategoryDoesExist(Id))
            {
                return await Save();
            }

            var deletedItem = await GetCategoryById(Id);

            _ctx.Categories?.Remove(deletedItem);

            return await Save();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return _ctx.Categories.Where(c=> c.Name != null);
        }

        public async Task<Category> GetCategoryById(Guid Id)
        {
            return await _ctx.Categories.FirstOrDefaultAsync(c=> c.Id == Id);
        }

        public async Task<Category> GetCategoryByName(string Name)
        {
            return await _ctx.Categories.FirstOrDefaultAsync(c => c.Name == Name);
        }

        public async Task<bool> Save()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<Category> UpdateCategory(Category Category)
        {
            _ctx.Categories.Update(Category);

            await Save();

            return Category;
        }
    }
}
