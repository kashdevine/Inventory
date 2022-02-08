using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Inventory.Contracts;
using Microsoft.Extensions.Logging;
using Inventory.Controllers.Api.v1;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTests.CategoriesTests.Api.v1
{
    public class CategoriesControllerTests
    {
        private InventoryContext _ctx;

        public CategoriesControllerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                        .UseSqlServer(connectionString: Utility.ConnectionString)
                        .Options;

            _ctx = new InventoryContext(contextOptions);
        }

        [Fact]
        public async Task GetCategories_API_Returns_JsonOfCategories()
        {
            //arrange
            var MockCategoryRepo = new Mock<ICategoryRepository>();
            MockCategoryRepo.Setup(cr => cr.GetCategories()).ReturnsAsync(Utility.SeedCategories());

            var mockLogger = new Mock<ILogger<CategoriesController>>();

            var categoryRepo = MockCategoryRepo.Object;
            var loggerInjection = mockLogger.Object;

            var categoryController = new CategoriesController(categoryRepo, loggerInjection);

            //act
            var result = await categoryController.GetCategories();

            //assert
            MockCategoryRepo.Verify(cr => cr.GetCategories(), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
