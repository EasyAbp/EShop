using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductCategories;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ProductCategories
{
    public class ProductCategoryRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;

        public ProductCategoryRepositoryTests()
        {
            _productCategoryRepository = GetRequiredService<IRepository<ProductCategory, Guid>>();
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
