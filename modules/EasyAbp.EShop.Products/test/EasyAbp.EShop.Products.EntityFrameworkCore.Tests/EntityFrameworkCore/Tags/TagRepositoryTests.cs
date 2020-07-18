using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Tags;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.Tags
{
    public class TagRepositoryTests : ProductsEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Tag, Guid> _tagRepository;

        public TagRepositoryTests()
        {
            _tagRepository = GetRequiredService<IRepository<Tag, Guid>>();
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
