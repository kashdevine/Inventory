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
    }
}
