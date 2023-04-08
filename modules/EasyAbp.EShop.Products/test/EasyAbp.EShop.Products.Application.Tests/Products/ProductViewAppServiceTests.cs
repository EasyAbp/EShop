using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductViewAppServiceTests : ProductsApplicationTestBase
    {
        private readonly IProductManager _productManager;
        private readonly IProductViewAppService _productViewAppService;

        public ProductViewAppServiceTests()
        {
            _productManager = GetRequiredService<IProductManager>();
            _productViewAppService = GetRequiredService<IProductViewAppService>();
        }

        [Fact]
        public async Task Should_Get_Product_Min_Max_Prices()
        {
            var getListResult = await _productViewAppService.GetListAsync(new GetProductListInput
            {
                StoreId = ProductsTestData.Store1Id
            });
            
            getListResult.Items.ShouldNotBeEmpty();

            var productDto = getListResult.Items.FirstOrDefault(x => x.Id == ProductsTestData.Product1Id);

            productDto.ShouldNotBeNull();
            productDto.MinimumPriceWithoutDiscount.ShouldBe(1m);
            productDto.MaximumPriceWithoutDiscount.ShouldBe(3m);
            
            var getResult = await _productViewAppService.GetAsync(ProductsTestData.Product1Id);

            getResult.ShouldNotBeNull();
            getResult.MinimumPriceWithoutDiscount.ShouldBe(1m);
            getResult.MaximumPriceWithoutDiscount.ShouldBe(3m);
        }


    }
}