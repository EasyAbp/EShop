using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductHistories;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ProductHistories
{
    public class ProductHistoryRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<ProductHistory, Guid> _productHistoryRepository;

        public ProductHistoryRepositoryTests()
        {
            _productHistoryRepository = GetRequiredService<IRepository<ProductHistory, Guid>>();
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
