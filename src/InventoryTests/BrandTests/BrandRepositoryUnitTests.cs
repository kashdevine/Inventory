using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Contracts;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Xunit;
using Microsoft.Extensions.Configuration;

namespace InventoryTests.BrandTests
{
    [Collection("InventoryTests")]
    public class BrandRepositoryUnitTests : IDisposable
    {
        private readonly IBrandRepository _brandRepository;
        private readonly InventoryContext? _ctx;

        public BrandRepositoryUnitTests(IBrandRepository brandRepository) 
        {
            ConfigurationManager config = new ConfigurationManager();

            var connString = config.GetConnectionString("TestInventoryDatabase");

            var contextOptions = new DbContextOptionsBuilder<InventoryContext>()
                                                .UseSqlServer(connString)
                                                .Options;
            _brandRepository = brandRepository;

            InventoryContext _ctx = new InventoryContext(contextOptions);
        }

        public void Dispose()
        {
            _ctx.Database.EnsureDeleted();
            _ctx.Database.EnsureCreated();
            Utility.SeedDB(_ctx);
            _ctx.Dispose();

        }
    }
}
