using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Inventory.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryTests.VendorTests.Api.v1
{
    internal class VendorControllerTests
    {
        private InventoryContext _ctx;
        public VendorControllerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                        .UseSqlServer(connectionString: Utility.ConnectionString)
                        .Options;
            _ctx = new InventoryContext(contextOptions);
        }
    }
}
