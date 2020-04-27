using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductStores;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ProductStores
{
    public class ProductStoreRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<ProductStore, Guid> _productStoreRepository;

        public ProductStoreRepositoryTests()
        {
            _productStoreRepository = GetRequiredService<IRepository<ProductStore, Guid>>();
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
