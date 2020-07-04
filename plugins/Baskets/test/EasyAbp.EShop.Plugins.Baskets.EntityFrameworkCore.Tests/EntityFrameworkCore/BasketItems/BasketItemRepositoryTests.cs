using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore.BasketItems
{
    public class BasketItemRepositoryTests : BasketsEntityFrameworkCoreTestBase
    {
        private readonly IBasketItemRepository _basketItemRepository;

        public BasketItemRepositoryTests()
        {
            _basketItemRepository = GetRequiredService<IBasketItemRepository>();
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
