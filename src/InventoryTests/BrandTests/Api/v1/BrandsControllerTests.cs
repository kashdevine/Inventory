using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Inventory.Contracts;
using Inventory.Models;
using Inventory.Controllers.Api.v1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using Inventory.Models.DTOs.Brand;
using Inventory.Services;

namespace InventoryTests.BrandTests.Api.v1
{
    public class BrandsControllerTests
    {
        private InventoryContext _ctx;

        public BrandsControllerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                                    .UseSqlServer(connectionString: Utility.ConnectionString)
                                    .Options;
            _ctx = new InventoryContext(contextOptions);
        }

        [Fact]
        public async Task GetBrands_API_Returns_JsonOfBrands()
        {
            //arrange
            var MockBrandRepo = new Mock<IBrandRepository>();
            MockBrandRepo.Setup(br => br.GetBrands()).ReturnsAsync(Utility.SeedBrands());

            var MockLogger = new Mock<ILogger<BrandsController>>();

            var BrandRepo = MockBrandRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new BrandsController(BrandRepo, LoggerInjection);

            //act
            var result = await _brandsController.GetBrands();

            //assert
            MockBrandRepo.Verify(br => br.GetBrands(), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetBrandWithId_API_Returns_JsonOfBrand()
        {
            //arrange
            var returnedBrand = Utility.getBrandForId(_ctx);
            var MockBrandRepo = new Mock<IBrandRepository>();
            MockBrandRepo.Setup(br => br.GetBrandById(returnedBrand.Result.Id)).Returns(returnedBrand);

            var MockLogger = new Mock<ILogger<BrandsController>>();

            var BrandRepo = MockBrandRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new BrandsController(BrandRepo, LoggerInjection);

            //act
            var result = await _brandsController.GetBrand(returnedBrand.Result.Id);

            //assert
            MockBrandRepo.Verify(br => br.GetBrandById(returnedBrand.Result.Id), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }


        [Fact]
        public async Task CreateBrand_API_Returns_JsonOfBrand()
        {
            //arrange
            var MockLogger = new Mock<ILogger<BrandsController>>();

            Brand Brand = new Brand() { Name = "New Brand"};
            var brandReturned = Utility.getBrandForId(_ctx);

            var brandRequest = new BrandCreateRequestDTO() { Name = "New Brand"};

            var MockBrandRepo = new Mock<IBrandRepository>();
            MockBrandRepo.Setup(br => br.CreateBrand(new Brand())).Returns(brandReturned);


            var BrandRepo = MockBrandRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new BrandsController(BrandRepo, LoggerInjection);

            //act
            var result = await _brandsController.CreateBrand(brandRequest);

            //assert
            MockBrandRepo.Verify(br => br.CreateBrand(It.IsAny<Brand>()), Times.Once);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task UpdateBrand_API_Returns_JsonOfUpdatedBrand()
        {
            //arrange
            var brandReturned = Utility.getBrandForId(_ctx);

            var brandRequest = new BrandUpdateRequestDTO();
            brandRequest.Name = "New Brand";

            var MockBrandRepo = new Mock<IBrandRepository>();
            MockBrandRepo.Setup(br => br.UpdateBrand(new Brand())).Returns(brandReturned);

            var MockLogger = new Mock<ILogger<BrandsController>>();

            var BrandRepo = MockBrandRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new BrandsController(BrandRepo, LoggerInjection);

            //act
            var result = await _brandsController.UpdateBrand(new Guid(), brandRequest);

            //assert
            MockBrandRepo.Verify(br => br.UpdateBrand(It.IsAny<Brand>()), Times.Once);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task DeleteBrand_API_Returns_TrueIfExistingBrandIsDeleted()
        {
            //arrange
            var MockBrandRepo = new Mock<IBrandRepository>();
            MockBrandRepo.Setup(br => br.DeleteBrand(new Guid())).ReturnsAsync(true);

            var MockLogger = new Mock<ILogger<BrandsController>>();

            var BrandRepo = MockBrandRepo.Object;
            var LoggerInjection = MockLogger.Object;

            var _brandsController = new BrandsController(BrandRepo, LoggerInjection);

            //act
            var result = await _brandsController.DeleteBrand(new Guid());

            //assert
            MockBrandRepo.Verify(br => br.DeleteBrand(It.IsAny<Guid>()), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }
    }
}
