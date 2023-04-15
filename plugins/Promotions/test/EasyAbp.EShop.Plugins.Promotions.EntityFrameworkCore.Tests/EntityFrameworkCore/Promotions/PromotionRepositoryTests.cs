using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore.Promotions;

public class PromotionRepositoryTests : PromotionsEntityFrameworkCoreTestBase
{
    private readonly IPromotionRepository _promotionRepository;

    public PromotionRepositoryTests()
    {
        _promotionRepository = GetRequiredService<IPromotionRepository>();
    }

    /*
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
    */
}
