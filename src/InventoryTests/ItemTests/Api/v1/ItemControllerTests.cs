using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using Inventory.Contracts;
using Inventory.Controllers.Api.v1;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Inventory.Models;
using Inventory.Models.DTOs.Item;

namespace InventoryTests.ItemTests.Api.v1
{
    [Collection("InventoryTests")]

    public class ItemControllerTests
    {
        private InventoryContext _ctx;

        public ItemControllerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
            .UseSqlServer(connectionString: Utility.ConnectionString)
            .Options;
            _ctx = new InventoryContext(contextOptions);
        }

        [Fact]
        public async Task GetItems_API_Returns_JsonofItems() 
        {
            //arrange
            var MockItemRepo = new Mock<IItemRepository>();
            MockItemRepo.Setup(ir => ir.GetItems()).ReturnsAsync(Utility.SeedItems());

            var MockLogger = new Mock<ILogger<ItemsController>>();

            var ItemRepo = MockItemRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var itemsController = new ItemsController(MockItemRepo.Object, LoggerInjection);
            //act
            var result = await itemsController.GetItems();

            //assert
            MockItemRepo.Verify(ir => ir.GetItems(), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetItem_API_Returns_JsonofItem()
        {
            //arrange
            var MockItemRepo = new Mock<IItemRepository>();
            MockItemRepo.Setup(ir => ir.GetItemById(It.IsAny<Guid>())).Returns(Utility.GetItemForId(_ctx));

            var MockLogger = new Mock<ILogger<ItemsController>>();

            var ItemRepo = MockItemRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var itemsController = new ItemsController(MockItemRepo.Object, LoggerInjection);
            
            //act
            var result = await itemsController.GetItem(new Guid());

            //assert
            MockItemRepo.Verify(ir => ir.GetItemById(It.IsAny<Guid>()), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateItem_API_Returns_JsonOfItem()
        {
            //arrange
            var mockCreateDTO = new ItemCreateRequestDTO();
            var MockItemRepo = new Mock<IItemRepository>();
            MockItemRepo.Setup(ir => ir.CreateItem(It.IsAny<Item>())).Returns(Utility.GetItemForId(_ctx));

            var MockLogger = new Mock<ILogger<ItemsController>>();

            var ItemRepo = MockItemRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var itemsController = new ItemsController(MockItemRepo.Object, LoggerInjection);
            
            //act
            var result = await itemsController.CreateItem(mockCreateDTO);

            //assert
            MockItemRepo.Verify(ir => ir.CreateItem(It.IsAny<Item>()), Times.Once);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task UpdateItem_API_Returns_JsonOfUpdatedItem()
        {
            //arrange
            var mockCreateDTO = new ItemUpdateRequestDTO();
            var itemWithGuid = await Utility.GetItemForId(_ctx);

            mockCreateDTO.Id = itemWithGuid.Id;

            var MockItemRepo = new Mock<IItemRepository>();
            MockItemRepo.Setup(ir => ir.UpdateItem(It.IsAny<Item>())).Returns(Utility.GetItemForId(_ctx));

            var MockLogger = new Mock<ILogger<ItemsController>>();

            var ItemRepo = MockItemRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var itemsController = new ItemsController(MockItemRepo.Object, LoggerInjection);

            //act
            var result = await itemsController.UpdateItem(itemWithGuid.Id, mockCreateDTO);

            //assert
            MockItemRepo.Verify(ir => ir.UpdateItem(It.IsAny<Item>()), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}
