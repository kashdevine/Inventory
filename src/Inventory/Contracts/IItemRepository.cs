using Inventory.Models;

namespace Inventory.Contracts
{
    /// <summary>
    /// This is responsible for handling the data connection for
    /// the items.
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// Gets all the items in the database.
        /// </summary>
        /// <returns>IEnumerable of items.</returns>
        public Task<IEnumerable<Item>> GetItems();

        /// <summary>
        /// Gets an Item based on the Id.
        /// </summary>
        /// <param name="Id">A Guid</param>
        /// <returns>An Item object.</returns>
        public Task<Item> GetItemById(Guid Id);

        /// <summary>
        /// Gets an Item based on the name.
        /// </summary>
        /// <param name="Name">A string.</param>
        /// <returns>An Item object.</returns>
        public Task<Item> GetItemByName(string Name);

        /// <summary>
        /// Adds an Item to the database and returns it.
        /// </summary>
        /// <param name="Item">Item Object.</param>
        /// <returns>Newly Created Item.</returns>
        public Task<Item> CreateItem(Item Item);

        /// <summary>
        /// Checks whether an Item exists by the name.
        /// </summary>
        /// <param name="ItemName">A string.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> ItemDoesExist(string ItemName);

        /// <summary>
        /// Checks whether an Item exists in the database.
        /// </summary>
        /// <param name="ItemId">A Guid.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> ItemDoesExist(Guid ItemId);

        /// <summary>
        /// Updates an item in the database and returns it.
        /// </summary>
        /// <param name="Item">Item Object.</param>
        /// <returns>Updated Item</returns>
        public Task<Item> UpdateItem(Item Item);

        /// <summary>
        /// Removes Item from the database.
        /// </summary>
        /// <param name="Id">A Guid.</param>
        /// <returns>A boolean.</returns>
        public Task<bool> DeleteItem(Guid Id);

        /// <summary>
        /// Saves changes to the database fi there were any.
        /// </summary>
        /// <returns>A boolean.</returns>
        public Task<bool> Save();
    }
}
