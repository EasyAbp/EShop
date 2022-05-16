using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public class ProductAssetCategoryAppServiceTests : BookingApplicationTestBase
    {
        private readonly IProductAssetCategoryAppService _productAssetCategoryAppService;

        public ProductAssetCategoryAppServiceTests()
        {
            _productAssetCategoryAppService = GetRequiredService<IProductAssetCategoryAppService>();
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
