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
using Castle.Core.Logging;
using Inventory.Controllers.Api.v1;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

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
    }
}
