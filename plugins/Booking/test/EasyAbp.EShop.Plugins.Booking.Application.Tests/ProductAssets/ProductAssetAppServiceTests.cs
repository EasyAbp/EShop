using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public class ProductAssetAppServiceTests : BookingApplicationTestBase
    {
        private readonly IProductAssetAppService _productAssetAppService;

        public ProductAssetAppServiceTests()
        {
            _productAssetAppService = GetRequiredService<IProductAssetAppService>();
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
