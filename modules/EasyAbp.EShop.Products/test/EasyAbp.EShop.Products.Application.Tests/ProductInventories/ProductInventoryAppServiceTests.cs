using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public class ProductInventoryAppServiceTests : ProductsApplicationTestBase
    {
        private readonly IProductInventoryAppService _productInventoryAppService;

        public ProductInventoryAppServiceTests()
        {
            _productInventoryAppService = GetRequiredService<IProductInventoryAppService>();
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
