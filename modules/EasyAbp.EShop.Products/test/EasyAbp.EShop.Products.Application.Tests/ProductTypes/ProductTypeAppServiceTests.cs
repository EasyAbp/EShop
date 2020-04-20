using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public class ProductTypeAppServiceTests : ProductsApplicationTestBase
    {
        private readonly IProductTypeAppService _productTypeAppService;

        public ProductTypeAppServiceTests()
        {
            _productTypeAppService = GetRequiredService<IProductTypeAppService>();
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
