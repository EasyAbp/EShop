using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Products.Tags
{
    public class TagAppServiceTests : ProductsApplicationTestBase
    {
        private readonly ITagAppService _tagAppService;

        public TagAppServiceTests()
        {
            _tagAppService = GetRequiredService<ITagAppService>();
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
