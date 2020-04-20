using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.Categories
{
    public class CategoryRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryRepositoryTests()
        {
            _categoryRepository = GetRequiredService<IRepository<Category, Guid>>();
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
