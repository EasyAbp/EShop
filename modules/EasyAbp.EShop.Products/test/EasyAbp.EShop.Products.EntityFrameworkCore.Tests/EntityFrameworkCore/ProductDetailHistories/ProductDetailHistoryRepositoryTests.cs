using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetailHistories;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ProductDetailHistories
{
    public class ProductDetailHistoryRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<ProductDetailHistory, Guid> _productDetailHistoryRepository;

        public ProductDetailHistoryRepositoryTests()
        {
            _productDetailHistoryRepository = GetRequiredService<IRepository<ProductDetailHistory, Guid>>();
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
