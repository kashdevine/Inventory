using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryTests.VendorTests
{
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
    }
}
