using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes.SimpleProductDiscount;
using EasyAbp.EShop.Products.Products;
using Shouldly;
using Volo.Abp.Json;
using Xunit;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public class SimpleProductDiscountTests : PromotionsApplicationTestBase
{
    private IJsonSerializer JsonSerializer { get; }
    private PromotionManager PromotionManager { get; }
    private SimpleProductDiscountPromotionHandler Handler { get; }

    public SimpleProductDiscountTests()
    {
        JsonSerializer = GetRequiredService<IJsonSerializer>();
        PromotionManager = GetRequiredService<PromotionManager>();
        Handler = GetRequiredService<SimpleProductDiscountPromotionHandler>();
    }

    [Fact]
    public async Task Should_Add_Candidate_Product_Discount()
    {
        var promotion = await CreatePromotionAsync();

        var productSku = new ProductSkuEto
        {
            Currency = "USD",
            Price = 1.00m,
        };

        var product = new ProductEto
        {
            ProductGroupName = "MyProductGroup",
            ProductSkus = new List<ProductSkuEto> { productSku }
        };

        var model = new ProductRealTimePriceInfoModel(product.Id, productSku.Id, 1.00m);

        await Handler.HandleProductAsync(model, promotion, product, productSku);

        model.CandidateProductDiscounts.Count.ShouldBe(1);

        var productDiscount = model.CandidateProductDiscounts.First();
        productDiscount.ShouldNotBeNull();
        productDiscount.EffectGroup.ShouldBe(PromotionConsts.PromotionEffectGroup);
        productDiscount.DisplayName.ShouldBe("test");
        productDiscount.DynamicDiscountAmount.DiscountAmount.ShouldBe(0.01m);
        productDiscount.DynamicDiscountAmount.DiscountRate.ShouldBe(0m);
        productDiscount.DynamicDiscountAmount.CalculatorName.ShouldBeNull();
        productDiscount.FromTime.ShouldBeNull();
        productDiscount.ToTime.ShouldBeNull();
    }

    private async Task<Promotion> CreatePromotionAsync()
    {
        return await PromotionManager.CreateAsync(Guid.NewGuid(),
            MinQuantityOrderDiscountPromotionHandler.MinQuantityOrderDiscountPromotionTypeName, "test", "test",
            JsonSerializer.Serialize(new SimpleProductDiscountConfigurations
            {
                Discounts = new List<SimpleProductDiscountModel>
                {
                    new(new List<ProductScopeModel>
                    {
                        new("MyProductGroup", null, null, null)
                    }, new DynamicDiscountAmountModel("USD", 0.01m, 0m, null))
                }
            }), null, null, false, 10000);
    }
}