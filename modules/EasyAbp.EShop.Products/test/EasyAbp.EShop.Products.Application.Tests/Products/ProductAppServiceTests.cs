using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAppServiceTests : ProductsApplicationTestBase
    {
        private readonly IProductAppService _productAppService;

        public ProductAppServiceTests()
        {
            _productAppService = GetRequiredService<IProductAppService>();
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
