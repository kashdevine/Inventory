using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Xunit;
using Inventory.Models;
using Inventory.Services;
using System;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;

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
        public async void GetBrand_Should_ReturnBrands()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            //act
            var result = await new BrandRepository(_ctx).GetBrands();           
            //assert
            Assert.True(result.GetEnumerator().MoveNext());
        }

        [Fact]
        public async void GetBrandByName_Should_ReturnBrand()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
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
            await Utility.ReinitializeDBForTests(_ctx);
            // Going to have to get the id of a brand to make sure that the implementation of the fetch function works
            Brand brand = await Utility.getBrandForId(_ctx);
            Guid brandId = brand.Id;
            // Act
            Brand result = await new BrandRepository(_ctx).GetBrandById(brandId);
            // Assert
            Assert.Equal(brandId, result.Id);
        }

        [Fact]
        public async void CreateBrand_Should_ReturnCreatedBrand()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Brand expected = new Brand() { Name="NewBrand"};
            //act
            Brand result = await new BrandRepository(_ctx).CreateBrand(expected);
            //assert
            Assert.Equal(expected,result);
        }

        [Fact]
        public async void BrandDoesExist_Should_TrueIfBrandExists()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            string brandThatExists = "Brand1";
            //act
            var result = await new BrandRepository(_ctx).BrandDoesExist(brandThatExists);
            //assert
            Assert.True(result);
        }

        [Fact]
        public async void BrandDoesExist_WithID_Should_TrueIfBrandExists()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Brand brandThatExists = await Utility.getBrandForId(_ctx);
            Guid id = brandThatExists.Id;
            //act
            var result = await new BrandRepository(_ctx).BrandDoesExist(id);
            //assert
            Assert.True(result);
        }


        [Fact]
        public async void DeleteBrand_Should_ReturnTrueIfBrandIsDeleted()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Brand brandThatExists = await Utility.getBrandForId(_ctx);
            Guid id = brandThatExists.Id;
            //act
            var result = await new BrandRepository(_ctx).DeleteBrand(id);
            //assert
            Assert.True(result);
        }

        [Fact]
        public async void DeleteBrand_Should_ReturnFalseIfBrandIsNotDeleted()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Guid idThatDoesNotExist = new Guid();
            //act
            var result = await new BrandRepository(_ctx).DeleteBrand(idThatDoesNotExist);
            //assert
            Assert.False(result);
        }

        [Fact]
        public async void UpdateBrand_Should_ReturnModifiedBrand()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Brand brandThatExists = await Utility.getBrandForId(_ctx);
            string expected = "ChangedBrandName";
            brandThatExists.Name = expected;
            //act
            var result = await new BrandRepository(_ctx).UpdateBrand(brandThatExists);
            //assert
            Assert.Equal(expected, result.Name);
        }
    }
}
