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

namespace InventoryTests.BrandTests.Api.v1
{
    public class BrandsControllerTests
    {

        [Fact]
        public async Task GetBrands_API_Returns_JsonOfBrands()
        {
            //arrange
            var MockBrandRepo = new Mock<IBrandRepository>();
            MockBrandRepo.Setup(br => br.GetBrands()).Returns(SeedBrandsAsync());

            var BrandRepo = MockBrandRepo.Object;

            var _brandsController = new BrandsController(BrandRepo);

            //act
            var result = await _brandsController.GetBrands();

            //assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        public static async Task<IEnumerable<Brand>> SeedBrandsAsync()
        {
            return new List<Brand>()
            {
                new Brand(){ Name = "Brand1"},
                new Brand(){ Name = "Brand2"},
                new Brand(){ Name = "Brand3"},
                new Brand(){ Name = "Brand4"}
            };
        }
    }
}
