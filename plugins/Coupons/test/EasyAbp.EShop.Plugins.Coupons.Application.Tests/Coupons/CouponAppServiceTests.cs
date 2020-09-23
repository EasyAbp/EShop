using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponAppServiceTests : CouponsApplicationTestBase
    {
        private readonly ICouponAppService _couponAppService;

        public CouponAppServiceTests()
        {
            _couponAppService = GetRequiredService<ICouponAppService>();
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
