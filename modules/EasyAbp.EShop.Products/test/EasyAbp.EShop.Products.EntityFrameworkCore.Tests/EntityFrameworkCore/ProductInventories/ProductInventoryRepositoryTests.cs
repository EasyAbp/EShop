using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ProductInventories
{
    public class ProductInventoryRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IProductInventoryRepository _productInventoryRepository;

        public ProductInventoryRepositoryTests()
        {
            _productInventoryRepository = GetRequiredService<IProductInventoryRepository>();
        }

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
    }
}
