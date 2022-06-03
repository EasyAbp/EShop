using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories
{
    public class StoreAssetCategoryAppServiceTests : BookingApplicationTestBase
    {
        private readonly IStoreAssetCategoryAppService _storeAssetCategoryAppService;

        public StoreAssetCategoryAppServiceTests()
        {
            _storeAssetCategoryAppService = GetRequiredService<IStoreAssetCategoryAppService>();
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
