using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;

namespace Inventory.Services
{
    public class ItemRepository : IItemRepository
    {
        private readonly InventoryContext _ctx;

        public ItemRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }
        public Task<Item> CreateItem(Item Item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItem(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetItemByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetItems()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ItemDoesExist(string ItemName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ItemDoesExist(Guid ItemId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateItem(Item Item)
        {
            throw new NotImplementedException();
        }
    }
}
