using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public class ProductCategoryAppServiceTests : ProductsApplicationTestBase
    {
        private readonly IProductCategoryAppService _productCategoryAppService;

        public ProductCategoryAppServiceTests()
        {
            _productCategoryAppService = GetRequiredService<IProductCategoryAppService>();
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
