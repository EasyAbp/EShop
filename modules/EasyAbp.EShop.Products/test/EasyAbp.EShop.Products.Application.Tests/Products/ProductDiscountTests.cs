using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountTests : ProductsApplicationTestBase
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.AddTransient<IProductDiscountProvider, DemoProductDiscountProvider>();
        base.AfterAddApplication(services);
    }

    [Fact]
    public async Task Should_Get_Product_With_Discount()
    {
        var productAppService = GetRequiredService<IProductAppService>();

        var product1 = await productAppService.GetAsync(ProductsTestData.Product1Id);
        var sku1 = (ProductSkuDto)product1.GetSkuById(ProductsTestData.Product1Sku1Id);
        var sku2 = (ProductSkuDto)product1.GetSkuById(ProductsTestData.Product1Sku2Id);

        sku1.Price.ShouldBe(1m - 0.01m * 4);
        sku1.ProductDiscounts.Count.ShouldBe(7);
        sku1.ProductDiscounts.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "1");
        sku1.ProductDiscounts.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "2");
        sku1.ProductDiscounts.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "3");
        sku1.ProductDiscounts.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "4");
        sku1.ProductDiscounts.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "5");
        sku1.ProductDiscounts.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "6");
        sku1.ProductDiscounts.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "7");
        sku1.OrderDiscountPreviews.Count.ShouldBe(2);
        sku1.OrderDiscountPreviews.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "1");
        sku1.OrderDiscountPreviews.ShouldContain(x => x.Name == "DemoDiscount" && x.Key == "2");

        sku2.Price.ShouldBe(2m);
        sku2.ProductDiscounts.ShouldBeEmpty();
        sku2.OrderDiscountPreviews.ShouldBeEmpty();
    }
}