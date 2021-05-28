using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Inventory.Suppliers;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Inventory.EntityFrameworkCore.Suppliers
{
    public class SupplierRepositoryTests : InventoryEntityFrameworkCoreTestBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierRepositoryTests()
        {
            _supplierRepository = GetRequiredService<ISupplierRepository>();
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
