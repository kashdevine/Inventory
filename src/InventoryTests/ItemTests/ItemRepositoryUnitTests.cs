using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryTests.ItemTests
{
    [Collection("InventoryTests")]
    public class ItemRepositoryUnitTests
    {
        private InventoryContext _ctx;

        public ItemRepositoryUnitTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
            .UseSqlServer(connectionString: Utility.ConnectionString)
            .Options;
            _ctx = new InventoryContext(contextOptions);
        }


    }
}
