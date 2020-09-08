using System.Linq;
using Shouldly;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores.Dtos;
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
        public async Task Should_Create_A_Store()
        {
            // Arrange
            var request = new CreateUpdateStoreDto
            {
                Name = "New Store 2"
            };

            // Act
            var response = await _storeAppService.CreateAsync(request);

            // Assert
            response.ShouldNotBeNull();
            response.Name.ShouldBe("New Store 2");
            
            UsingDbContext(db =>
            {
                var store = db.Stores.FirstOrDefault(x=>x.Id == response.Id);
                store.ShouldNotBeNull();
                store.Name.ShouldBe("New Store 2");
            });
        }

        [Fact]
        public async Task Should_Return_Default_Store()
        {
            // Arrange & Act
            var response = await _storeAppService.GetDefaultAsync();
            
            // Assert
            response.ShouldNotBeNull();
            response.Name.ShouldBe("My Store");
        }
    }
}
