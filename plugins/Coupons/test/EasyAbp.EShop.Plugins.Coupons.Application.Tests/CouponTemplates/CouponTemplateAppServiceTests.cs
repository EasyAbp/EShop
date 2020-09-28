using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateAppServiceTests : CouponsApplicationTestBase
    {
        private readonly ICouponTemplateAppService _couponTemplateAppService;

        public CouponTemplateAppServiceTests()
        {
            _couponTemplateAppService = GetRequiredService<ICouponTemplateAppService>();
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
}
