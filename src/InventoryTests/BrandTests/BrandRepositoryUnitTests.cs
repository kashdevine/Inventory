using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Xunit;
using Inventory.Models;
using Inventory.Services;

namespace InventoryTests.BrandTests
{
    [Collection("InventoryTests")]
    public class BrandRepositoryUnitTests
    {
        private InventoryContext _ctx;
 

        public BrandRepositoryUnitTests() 
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                                                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestInventoryDb;Trusted_Connection=True;MultipleActiveResultSets=true")
                                                .Options;

             _ctx = new InventoryContext(contextOptions);
            


        }

        [Fact]
        public async void GetBrandByName_Should_ReturnBrand()
        {
            //arrange
            Utility.ReinitializeDBForTests(_ctx);

            string Name = "Brand1";
            //act
            Brand result = await new BrandRepository(_ctx).GetBrandByName(Name);
            //assert
            Assert.Equal(Name, result.Name);
        }

        [Fact]
        public async void GetBrandById_Should_ReturnBrand()
        {
            // Arrange
            // Going to have to get the id of a brand to make sure that the implementation of the fetch function works
            // Act
            // Assert
        }
    }
}
