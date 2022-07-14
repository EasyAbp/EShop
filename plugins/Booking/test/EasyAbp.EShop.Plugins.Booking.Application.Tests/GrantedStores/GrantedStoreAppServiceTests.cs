using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores
{
    public class GrantedStoreAppServiceTests : BookingApplicationTestBase
    {
        private readonly IGrantedStoreAppService _grantedStoreAppService;

        public GrantedStoreAppServiceTests()
        {
            _grantedStoreAppService = GetRequiredService<IGrantedStoreAppService>();
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
