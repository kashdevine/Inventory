﻿using Inventory.Contracts;
using Inventory.Data;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Services
{
    /// <summary>
    /// <inheritdoc cref="IItemRepository"/>
    /// </summary>
    public class ItemRepository : IItemRepository
    {
        private readonly InventoryContext _ctx;

        public ItemRepository(InventoryContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Item> CreateItem(Item Item)
        {
            if (Item == null)
            {
                throw new ArgumentNullException(nameof(Item));
            }

            if (await ItemDoesExist(Item.Name))
            {
                return await GetItemByName(Item.Name);
            }

            await _ctx.AddAsync(Item);

            
            if (await Save())
            {
                return Item;
            }

            return null;
        }

        public async Task<bool> DeleteItem(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentNullException(nameof(Id));
            }

            if (! await ItemDoesExist(Id))
            {
                return false;
            }

            _ctx.Items.Remove(await GetItemById(Id));
            return await Save();
        }

        public async Task<Item> GetItemById(Guid Id)
        {
            return await _ctx.Items.FirstOrDefaultAsync(i => i.Id == Id);

        }

        public async Task<Item> GetItemByName(string Name)
        {
            return await _ctx.Items.FirstOrDefaultAsync(x => x.Name == Name);
        }

        public async Task<IEnumerable<Item>> GetItems()
        {
            return await _ctx.Items.Where(i => i.Name != null).ToListAsync();
        }

        public async Task<bool> ItemDoesExist(string ItemName)
        {
            if (string.IsNullOrEmpty(ItemName))
            {
                throw new ArgumentException(nameof(ItemName));
            }

            return await _ctx.Items.AnyAsync(i => i.Name == ItemName);
        }

        public async Task<bool> ItemDoesExist(Guid ItemId)
        {
            if (ItemId == null)
            {
                throw new ArgumentNullException(nameof(ItemId));
            }

            return await _ctx.Items.AnyAsync(i=> i.Id == ItemId); 
        }

        public async Task<bool> Save()
        {
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<Item> UpdateItem(Item Item)
        {
            if (Item == null)
            {
                throw new ArgumentNullException(nameof(Item));
            }

            if (! await ItemDoesExist(Item.Id))
            {
                throw new ArgumentException(nameof(Item));
            }

            _ctx.Items.Update(Item);
            
            if (await Save())
            {
                return Item;
            }

            return null;
        }
    }
}
