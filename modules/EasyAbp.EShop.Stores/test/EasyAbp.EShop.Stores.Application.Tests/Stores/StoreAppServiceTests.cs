using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreAppServiceTests : StoresApplicationTestBase
    {
        private readonly IStoreAppService _storeAppService;

        public StoreAppServiceTests()
        {
            _storeAppService = GetRequiredService<IStoreAppService>();
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
