using Inventory.Models;

namespace Inventory.Contracts
{
    /// <summary>
    /// This is responsible for handling the data connection
    /// for the categories.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Gets a list of all the categories in the database.
        /// </summary>
        /// <returns>IEnumerable of the categories.</returns>
        public Task<IEnumerable<Category>> GetCategories();

        /// <summary>
        /// Gets a category based on the Id.
        /// </summary>
        /// <param name="Id">A Guid.</param>
        /// <returns>A Category object.</returns>
        public Task<Category> GetCategoryById(Guid Id);

        /// <summary>
        /// Gets a category based on the name.
        /// </summary>
        /// <param name="Name">A string.</param>
        /// <returns>A Category object.</returns>
        public Task<Category> GetCategoryByName(string Name);

        /// <summary>
        /// Adds a category to the database and returns it for use.
        /// </summary>
        /// <param name="Category"></param>
        /// <returns>A Category object.</returns>
        public Task<Category> CreateCategory(Category Category);

        /// <summary>
        /// Checks whether a Category exists by its name.
        /// </summary>
        /// <param name="CategoryName">A string.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> CategoryDoesExist(string CategoryName);

        /// <summary>
        /// Updates a Category in the database.
        /// </summary>
        /// <param name="Category">A Category Object.</param>
        /// <returns>Updated Category.</returns>
        public Task<Category> UpdateCategory(Category Category);

        /// <summary>
        /// Removes a Category from the datbase.
        /// </summary>
        /// <param name="Id">A Guid.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> DeleteCategory(Guid Id);

        /// <summary>
        /// Saves changes in the database if there were any.
        /// </summary>
        /// <returns>A boolean.</returns>
        public Task<bool> Save();
    }
}
