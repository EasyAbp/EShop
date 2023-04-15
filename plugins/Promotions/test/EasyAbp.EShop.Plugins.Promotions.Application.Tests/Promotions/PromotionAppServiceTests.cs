using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public class PromotionAppServiceTests : PromotionsApplicationTestBase
{
    private readonly IPromotionAppService _promotionAppService;

    public PromotionAppServiceTests()
    {
        _promotionAppService = GetRequiredService<IPromotionAppService>();
    }

    /*
    [Fact]
    public async Task Test1()
    {
        // Arrange

        // Act

        // Assert
    }
    */
}

