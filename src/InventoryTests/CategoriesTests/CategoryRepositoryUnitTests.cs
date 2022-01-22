using Inventory.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InventoryTests.CategoriesTests
{
    public class CategoryRepositoryUnitTests
    {
        private InventoryContext _ctx;

        public CategoryRepositoryUnitTests()
        {
            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                                    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=TestInventoryDb;Trusted_Connection=True;MultipleActiveResultSets=true")
                                    .Options;
            _ctx = new InventoryContext(contextOptions);
        }
    }
}
