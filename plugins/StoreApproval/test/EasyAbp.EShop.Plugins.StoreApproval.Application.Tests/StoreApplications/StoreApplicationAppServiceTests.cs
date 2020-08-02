using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public class StoreApplicationAppServiceTests : StoreApprovalApplicationTestBase
    {
        private readonly IStoreApplicationAppService _storeApplicationAppService;

        public StoreApplicationAppServiceTests()
        {
            _storeApplicationAppService = GetRequiredService<IStoreApplicationAppService>();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
