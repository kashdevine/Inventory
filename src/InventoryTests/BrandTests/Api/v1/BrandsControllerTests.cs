using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Inventory.Contracts;

namespace InventoryTests.BrandTests.Api.v1
{
    public class BrandsControllerTests
    {
        public BrandsControllerTests()
        {
            var MockBrandRepo = new Mock<IBrandRepository>();

            var BrandRepo = MockBrandRepo.Object;
        }
    }
}
