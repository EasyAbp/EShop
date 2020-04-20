using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductTypes;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ProductTypes
{
    public class ProductTypeRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<ProductType, Guid> _productTypeRepository;

        public ProductTypeRepositoryTests()
        {
            _productTypeRepository = GetRequiredService<IRepository<ProductType, Guid>>();
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
