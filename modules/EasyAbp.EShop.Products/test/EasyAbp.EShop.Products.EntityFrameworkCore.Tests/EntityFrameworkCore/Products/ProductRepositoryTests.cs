using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.Products
{
    public class ProductRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Product, Guid> _productRepository;

        public ProductRepositoryTests()
        {
            _productRepository = GetRequiredService<IRepository<Product, Guid>>();
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
