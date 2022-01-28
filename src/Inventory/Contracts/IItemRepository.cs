using Inventory.Models;

namespace Inventory.Contracts
{
    public interface IItemRepository
    {
        public Task<IEnumerable<Item>> GetItems();

        public Task<Item> GetItemById(Guid Id);

        public Task<Item> GetItemByName(string Name);

        public Task<Item> CreateItem(Item Item);

        public Task<bool> ItemDoesExist(string ItemName);

        public Task<bool> ItemDoesExist(Guid ItemId);

        public Task<Item> UpdateItem(Item Item);

        public Task<bool> DeleteItem(Guid Id);

        public Task<bool> Save();
    }
}
