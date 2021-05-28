using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Warehouses;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore.Warehouses
{
    public class WarehouseRepositoryTests : InventoryEntityFrameworkCoreTestBase
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseRepositoryTests()
        {
            _warehouseRepository = GetRequiredService<IWarehouseRepository>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
        */
    }
}
