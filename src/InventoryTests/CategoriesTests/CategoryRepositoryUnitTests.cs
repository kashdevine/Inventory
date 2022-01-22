using Inventory.Data;
using Inventory.Models;
using Inventory.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InventoryTests.CategoriesTests
{
    [Collection("InventoryTests")]
    public class CategoryRepositoryUnitTests
    {
        private InventoryContext _ctx;

        public CategoryRepositoryUnitTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                                    .UseSqlServer(connectionString:Utility.ConnectionString)
                                    .Options;
            _ctx = new InventoryContext(contextOptions);
        }

        [Fact]
        public async Task CreateCategory_Should_ReturnNewCategory()
        {
            // arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = new Category() { Name = "NewCategory" };

            // act
            var result = await new CategoryRepository(_ctx).CreateCategory(expected);

            //assert
            Assert.Equal(expected.Name, result.Name);
        }

        [Fact]
        public async Task CreateCategory_Should_CreateNewCategory()
        {
            // arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = new Category() { Name = "NewCategory" };

            // act
            var result = await new CategoryRepository(_ctx).CreateCategory(expected);

            //assert
            Assert.NotNull(result.Id);
        }

        [Fact]
        public async Task CreateCategory_ShouldNot_CreateNewCategoryIfItExists()
        {
            // arrange
            await Utility.ReinitializeDBForTests(_ctx);

            var expected = await _ctx.Categories.FirstAsync(c => c.Name == "Category1");
            var exisitingCategory = new Category() { Name = "Category1" };

            // act
            var result = await new CategoryRepository(_ctx).CreateCategory(exisitingCategory);

            //assert
            Assert.Equal(expected.Id, result.Id);
        }

        [Fact]
        public async Task CategoryDoesExist_Should_ReturnFalseIfCategoryDoesNotExist()
        {
            // arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = new Category() { Name = "NewCategory" };

            // act
            var result = await new CategoryRepository(_ctx).CategoryDoesExist(expected.Name);

            //assert
            Assert.False(result);
        }
        
        [Fact]
        public async Task CategoryDoesExist_Should_ReturnTrueIfCategoryDoesExist()
        {
            // arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = new Category() { Name = "Category1" };

            // act
            var result = await new CategoryRepository(_ctx).CategoryDoesExist(expected.Name);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetCategoryById_Should_ReturnSingleCategory()
        {
            // arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = await Utility.GetCategoryForId(_ctx);

            // act
            var result = await new CategoryRepository(_ctx).GetCategoryById(expected.Id);

            //assert
            Assert.Equal(expected.Id, result.Id);
        }

        [Fact]
        public async Task DeleteCategory_Should_ReturnTrueIfCategoryIsDeleted()
        {
            // arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var DeletedItem = await Utility.GetCategoryForId(_ctx);

            // act
            var result = await new CategoryRepository(_ctx).DeleteCategory(DeletedItem.Id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetCategories_Should_ReturnListOfCategories()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            //act
            var result = await new CategoryRepository(_ctx).GetCategories();

            //assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCategoryByName_Should_ReturnCategoryIfItExists()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = "Category1";
            //act
            var result = await new CategoryRepository(_ctx).GetCategoryByName(expected);

            //assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task GetCategoryByName_Should_ReturnNullIfItDoesNotExists()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = "Category5";
            //act
            var result = await new CategoryRepository(_ctx).GetCategoryByName(expected);

            //assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateCategory_Should_ReturnUpdatedCategory()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            var expected = await Utility.GetCategoryForId(_ctx);
            expected.Name = "UpdatedCategory";
            //act
            var result = await new CategoryRepository(_ctx).UpdateCategory(expected);

            //assert
            Assert.Equal(expected.Name, result.Name);
        }
    }
}
