using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Products.Products;

public class DemoProductDiscountProvider : IProductDiscountProvider
{
    private readonly IClock _clock;

    public DemoProductDiscountProvider(IClock clock)
    {
        _clock = clock;
    }

    public static int DemoProductDiscountEffectOrder { get; set; } = 10000;

    public int EffectOrder => DemoProductDiscountEffectOrder;

    public Task DiscountAsync(ProductDiscountContext context)
    {
        if (context.Product.Id != ProductsTestData.Product1Id ||
            context.ProductSku.Id != ProductsTestData.Product1Sku1Id)
        {
            return Task.CompletedTask;
        }

        var productDiscountInfoModels = new List<ProductDiscountInfoModel>
        {
            // These should take effect:
            new(null, "DemoDiscount", "1", "Demo Discount 1", 0.10m, null, null),
            new(null, "DemoDiscount", "2", "Demo Discount 2", 0.10m, _clock.Now.AddDays(-1), null),
            new(null, "DemoDiscount", "3", "Demo Discount 3", 0.10m, null, _clock.Now.AddDays(1)),
            new(null, "DemoDiscount", "4", "Demo Discount 4", 0.10m, _clock.Now.AddDays(-1), _clock.Now.AddDays(1)),
            // These should not take effect:
            new(null, "DemoDiscount", "5", "Demo Discount 5", 0.10m, null, _clock.Now.AddDays(-1)),
            new(null, "DemoDiscount", "6", "Demo Discount 6", 0.10m, _clock.Now.AddDays(1), null),
            new(null, "DemoDiscount", "7", "Demo Discount 7", 0.10m, _clock.Now.AddDays(1), _clock.Now.AddDays(2)),
            // Only the one with the highest discount amount should take effect: 
            new("A", "DemoDiscount", "8", "Demo Discount 8", 0.10m, null, null),
            new("A", "DemoDiscount", "9", "Demo Discount 9", 0.01m, null, null),
        };

        foreach (var model in productDiscountInfoModels)
        {
            context.AddOrUpdateProductDiscount(model);
        }

        var orderDiscountPreviewInfoModels = new List<OrderDiscountPreviewInfoModel>
        {
            new(null, "DemoDiscount", "1", "Demo Discount 1", null, null),
            new(null, "DemoDiscount", "2", "Demo Discount 2", _clock.Now.AddDays(-1), _clock.Now.AddDays(1)),
        };

        foreach (var model in orderDiscountPreviewInfoModels)
        {
            context.AddOrUpdateOrderDiscountPreview(model);
        }

        return Task.CompletedTask;
    }
}