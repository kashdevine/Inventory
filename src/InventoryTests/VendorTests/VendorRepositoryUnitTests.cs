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

namespace InventoryTests.VendorTests
{
    [Collection("InventoryTests")]
    public class VendorRepositoryUnitTests
    {
        private InventoryContext _ctx;

        public VendorRepositoryUnitTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                        .UseSqlServer(connectionString: Utility.ConnectionString)
                        .Options;
            _ctx = new InventoryContext(contextOptions);
        }

        [Fact]
        public async Task GetVendors_Should_ReturnListOfVendors()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            //act
            IEnumerable<Vendor> results = await new VendorRepository(_ctx).GetVendors();

            //assert
            Assert.NotEmpty(results);
        }

        [Fact]
        public async Task GetVendorByName_Should_ReturnSingleVendor()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            string expected = "Vendor1";
            //act
            Vendor result = await new VendorRepository(_ctx).GetVendorByName(expected);

            //assert
            Assert.Equal(expected, result.Name);
        }

        [Fact]
        public async Task GetVendorById_Should_ReturnSingleVendor()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Vendor expected = await Utility.GetVendorForId(_ctx);
            //act
            Vendor result = await new VendorRepository(_ctx).GetVendorById(expected.Id);

            //assert
            Assert.Equal(expected.Id, result.Id);
        }

        [Fact]
        public async Task VendorDoesExist_Should_ReturnTrueIfTheVendorExistsWithName()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            string expected = "Vendor1";
            //act
            bool result = await new VendorRepository(_ctx).VendorDoesExist(expected);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task VendorDoesExist_Should_ReturnFalseIfTheVendorDoesNotExistsWithName()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            string expected = "Vendor100";
            //act
            bool result = await new VendorRepository(_ctx).VendorDoesExist(expected);

            //assert
            Assert.False(result);
        }

        [Fact]
        public async Task VendorDoesExist_Should_ReturnTrueIfTheVendorExistsWithId()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Vendor expected = await Utility.GetVendorForId(_ctx);

            //act
            bool result = await new VendorRepository(_ctx).VendorDoesExist(expected.Id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task VendorDoesExist_Should_ReturnFalseIfTheVendorDoesNotExistsWithId()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Guid expected = Guid.NewGuid();

            //act
            bool result = await new VendorRepository(_ctx).VendorDoesExist(expected);

            //assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateVendor_Should_ReturnTheUpdatedVendor()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Vendor expected = await Utility.GetVendorForId(_ctx);
            expected.Name = "VendorUpdated";

            //act
            Vendor result = await new VendorRepository(_ctx).UpdateVendor(expected);

            //assert
            Assert.Equal(expected.Name, result.Name);
        }


        [Fact]
        public async Task DeleteVendor_Should_ReturnTrueIfVendorWasDeleted()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Vendor expected = await Utility.GetVendorForId(_ctx);

            //act
            bool result = await new VendorRepository(_ctx).DeleteVendor(expected.Id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateVendor_Should_ReturnsTheNewVendor()
        {
            //arrange
            await Utility.ReinitializeDBForTests(_ctx);
            Vendor expected = new Vendor() { Name = "NewVendor" };
            //act
            Vendor result = await new VendorRepository(_ctx).CreateVendor(expected);

            //assert
            Assert.NotNull(result.Id);
        }
    }
}
