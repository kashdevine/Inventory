using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Inventory.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryTests.ItemTests.Api.v1
{
    [Collection("InventoryTests")]

    public class ItemControllerTests
    {
        private InventoryContext _ctx;

        public ItemControllerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
            .UseSqlServer(connectionString: Utility.ConnectionString)
            .Options;
            _ctx = new InventoryContext(contextOptions);
        }
    }
}
