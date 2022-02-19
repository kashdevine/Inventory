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
using Inventory.Models.DTOs.Vendor;
using Inventory.Models;

namespace InventoryTests.VendorTests.Api.v1
{
    [Collection("InventoryTests")]
    public class VendorControllerTests
    {
        private InventoryContext _ctx;
        public VendorControllerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                        .UseSqlServer(connectionString: Utility.ConnectionString)
                        .Options;
            _ctx = new InventoryContext(contextOptions);
        }

        [Fact]
        public async Task GetVendors_API_Returns_JsonOfVendors()
        {
            //arrange
            var MockVendorRepo = new Mock<IVendorRepository>();
            MockVendorRepo.Setup(vr => vr.GetVendors()).ReturnsAsync(Utility.SeedVendors());

            var MockLogger = new Mock<ILogger<VendorsController>>();

            var VendorRepo = MockVendorRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new VendorsController(VendorRepo, LoggerInjection);

            //act
            var result = await _brandsController.GetVendors();

            //assert
            MockVendorRepo.Verify(vr => vr.GetVendors(), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetVendor_API_Returns_JsonOfVendor()
        {
            //arrange
            var MockVendorRepo = new Mock<IVendorRepository>();
            MockVendorRepo.Setup(vr => vr.GetVendorById(new Guid())).Returns(Utility.GetVendorForId(_ctx));

            var MockLogger = new Mock<ILogger<VendorsController>>();

            var VendorRepo = MockVendorRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new VendorsController(VendorRepo, LoggerInjection);

            //act
            var result = await _brandsController.GetVendor(new Guid());

            //assert
            MockVendorRepo.Verify(vr => vr.GetVendorById(It.IsAny<Guid>()), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task CreateVendor_API_Returns_JsonOfVendor()
        {
            //arrange
            var mockCreateDTO = new VendorCreateRequestDTO();
            var MockVendorRepo = new Mock<IVendorRepository>();
            MockVendorRepo.Setup(vr => vr.CreateVendor(It.IsAny<Vendor>())).Returns(Utility.GetVendorForId(_ctx));

            var MockLogger = new Mock<ILogger<VendorsController>>();

            var VendorRepo = MockVendorRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new VendorsController(VendorRepo, LoggerInjection);

            //act
            var result = await _brandsController.CreateVendor(mockCreateDTO);

            //assert
            MockVendorRepo.Verify(vr => vr.CreateVendor(It.IsAny<Vendor>()), Times.Once);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task UpdateVendor_API_Returns_JsonOfUPdatedVendor()
        {
            //arrange
            var mockCreateDTO = new VendorUpdateRequestDTO();
            var MockVendorRepo = new Mock<IVendorRepository>();
            MockVendorRepo.Setup(vr => vr.UpdateVendor(It.IsAny<Vendor>())).Returns(Utility.GetVendorForId(_ctx));

            var MockLogger = new Mock<ILogger<VendorsController>>();

            var VendorRepo = MockVendorRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new VendorsController(VendorRepo, LoggerInjection);

            //act
            var result = await _brandsController.UpdateVendor(new Guid(),mockCreateDTO);

            //assert
            MockVendorRepo.Verify(vr => vr.UpdateVendor(It.IsAny<Vendor>()), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteVendor_API_Returns_TrueIfExistingBrandIsDeleted()
        {
            //arrange
            var MockVendorRepo = new Mock<IVendorRepository>();
            MockVendorRepo.Setup(vr => vr.DeleteVendor(It.IsAny<Guid>())).ReturnsAsync(true);

            var MockLogger = new Mock<ILogger<VendorsController>>();

            var VendorRepo = MockVendorRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new VendorsController(VendorRepo, LoggerInjection);

            //act
            var result = await _brandsController.DeleteVendor(new Guid());

            //assert
            MockVendorRepo.Verify(vr => vr.DeleteVendor(It.IsAny<Guid>()), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }
    }
}
