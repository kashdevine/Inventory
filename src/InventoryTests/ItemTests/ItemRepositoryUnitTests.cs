using Inventory.Data;
using Inventory.Models;
using Inventory.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryTests.ItemTests
{
    [Collection("InventoryTests")]
    public class ItemRepositoryUnitTests
    {
        private InventoryContext _ctx;

        public ItemRepositoryUnitTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
            .UseSqlServer(connectionString: Utility.ConnectionString)
            .Options;
            _ctx = new InventoryContext(contextOptions);
        }
        
        [Fact]
        public async Task GetItemById_Should_ReturnItemWithThatId()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Item expected = await Utility.GetItemForId(_ctx);
            //act 
            Item result = await new ItemRepository(_ctx).GetItemById(expected.Id);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetItemByName_Should_ReturnItemWithThatName()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Item expected = await Utility.GetItemForId(_ctx);
            //act 
            Item result = await new ItemRepository(_ctx).GetItemByName(expected.Name);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetItems_Should_ReturnItemsNames()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            //act 
            IEnumerable<Item> result = await new ItemRepository(_ctx).GetItems();
            //assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task ItemDoesExist_Should_ReturnTrueIfItemExists()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var input = "Item1";
            //act 
            bool result = await new ItemRepository(_ctx).ItemDoesExist(input);
            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task ItemDoesExist_Should_ReturnFalseIfItemDoesNotExists()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var input = "Item100";
            //act 
            bool result = await new ItemRepository(_ctx).ItemDoesExist(input);
            //assert
            Assert.False(result);
        }


        [Fact]
        public async Task ItemDoesExist_Should_ReturnTrueIfItemExistsWithId()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Item input = await Utility.GetItemForId(_ctx);

            //act 
            bool result = await new ItemRepository(_ctx).ItemDoesExist(input.Id);
            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateItem_Should_ReturnCreatedItem()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Item input = new Item() { Name = "Item100", Description = "Item100 Description", PerUnitCost = 0, Price = 100, IsDigital = true };
            //act 
            Item result = await new ItemRepository(_ctx).CreateItem(input);
            //assert
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task UpdateItem_Should_ReturnUpdatedItem()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Item expected = await Utility.GetItemForId(_ctx);
            expected.Name = "Updated Item";
            //act 
            Item result = await new ItemRepository(_ctx).UpdateItem(expected);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task DeleteItem_Should_ReturnTrueIfItemIsDeleted()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Item expected = await Utility.GetItemForId(_ctx);
            
            //act 
            bool result = await new ItemRepository(_ctx).DeleteItem(expected.Id);
            //assert
            Assert.True(result);
        }

    }
}
