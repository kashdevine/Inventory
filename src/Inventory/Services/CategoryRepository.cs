using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services
{
    /// <summary>
    /// <inheritdoc cref="ICategoryRepository"/>
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryContext _ctx;

        public CategoryRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> CategoryDoesExist(string CategoryName)
        {
            if (string.IsNullOrEmpty(CategoryName))
            {
                throw new ArgumentException(nameof(CategoryName));
            }

            return  await _ctx.Categories.AnyAsync(c => c.Name == CategoryName);
            
        }
        /// <summary>
        /// Checks whether a category exists by its Id.
        /// </summary>
        /// <param name="CategoryId">A Guid.</param>
        /// <returns>A boolean.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<bool> CategoryDoesExist(Guid CategoryId)
        {
            if (CategoryId == null)
            {
                throw new ArgumentNullException(nameof(CategoryId));
            }

            return await _ctx.Categories.AnyAsync(c => c.Id == CategoryId);

        }

        public async Task<Category> CreateCategory(Category Category)
        {
            if (await CategoryDoesExist(Category.Name))
            {
                return await _ctx.Categories.FirstOrDefaultAsync(c => c.Name == Category.Name);
            }

            Category.CreatedAt = DateTime.UtcNow;
            Category.LastUpdated = DateTime.UtcNow;

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
            if (Category == null)
            {
                throw new ArgumentNullException(nameof(Category));
            }

            if (! await CategoryDoesExist(Category.Id))
            {
                throw new ArgumentException(nameof(Category));
            }
            
            Category.LastUpdated = DateTime.UtcNow;

            _ctx.Categories!.Update(Category);

            await Save();

            return Category;
        }
    }
}
