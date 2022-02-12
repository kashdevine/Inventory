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
using Inventory.Models.DTOs.Category;

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
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetCategory_API_Returns_JsonOfCategory()
        {
            //arrange
            var MockCategoryRepo = new Mock<ICategoryRepository>();
            MockCategoryRepo.Setup(cr => cr.GetCategoryById(new Guid())).Returns(Utility.GetCategoryForId(_ctx));

            var mockLogger = new Mock<ILogger<CategoriesController>>();

            var categoryRepo = MockCategoryRepo.Object;
            var loggerInjection = mockLogger.Object;

            var categoryController = new CategoriesController(categoryRepo, loggerInjection);
            //act
            var result = await categoryController.GetCategory(new Guid());

            //assert
            MockCategoryRepo.Verify(cr => cr.GetCategoryById(new Guid()), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateCategory_API_Returns_JsonofCategory()
        {
            //arrange
            var MockCategoryRepo = new Mock<ICategoryRepository>();
            MockCategoryRepo.Setup(cr => cr.CreateCategory(new Inventory.Models.Category())).Returns(Utility.GetCategoryForId(_ctx));

            var mockLogger = new Mock<ILogger<CategoriesController>>();

            var categoryRepo = MockCategoryRepo.Object;
            var loggerInjection = mockLogger.Object;

            var categoryController = new CategoriesController(categoryRepo, loggerInjection);
            //act
            var result = await categoryController.CreateCategory(new CategoryCreateRequestDTO() { Name = "testing"});

            //assert
            Assert.IsType<CreatedAtActionResult>(result.Result);

        }

        [Fact]
        public async Task UpdateCategory_API_Returns_JsonofCategory() 
        {
            //arrange
            var testCategory = await Utility.GetCategoryForId(_ctx);
            CategoryUpdateRequestDTO testUpdateObject = new CategoryUpdateRequestDTO() { Id = testCategory.Id, Name = testCategory.Name};

            var MockCategoryRepo = new Mock<ICategoryRepository>();
            MockCategoryRepo.Setup(cr => cr.UpdateCategory(new Inventory.Models.Category())).Returns(Utility.GetCategoryForId(_ctx));

            var mockLogger = new Mock<ILogger<CategoriesController>>();

            var categoryRepo = MockCategoryRepo.Object;
            var loggerInjection = mockLogger.Object;

            var categoryController = new CategoriesController(categoryRepo, loggerInjection);
            //act
            var result = await categoryController.UpdateCategory(testUpdateObject.Id, testUpdateObject);

            //assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteCategory_API_Returns_NoContentIfCategoryDeleted()
        {
            //arrange
            var testCategory = await Utility.GetCategoryForId(_ctx);

            var MockCategoryRepo = new Mock<ICategoryRepository>();
            MockCategoryRepo.Setup(cr => cr.DeleteCategory(testCategory.Id)).ReturnsAsync(true);

            var mockLogger = new Mock<ILogger<CategoriesController>>();

            var categoryRepo = MockCategoryRepo.Object;
            var loggerInjection = mockLogger.Object;

            var categoryController = new CategoriesController(categoryRepo, loggerInjection);
            //act
            var result = await categoryController.DeleteCategory(testCategory.Id);

            //assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
