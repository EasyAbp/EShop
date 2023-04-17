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

    public Task DiscountAsync(GetProductsRealTimePriceContext context)
    {
        foreach (var model in context.Models.Values)
        {
            if (model.ProductId != ProductsTestData.Product1Id ||
                model.ProductSkuId != ProductsTestData.Product1Sku1Id)
            {
                return Task.CompletedTask;
            }

            var candidates = new List<CandidateProductDiscountInfoModel>
            {
                // These should take effect:
                new(null, "DemoDiscount", "1", "Demo Discount 1",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), null, null),
                new(null, "DemoDiscount", "2", "Demo Discount 2",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), _clock.Now.AddDays(-1), null),
                new(null, "DemoDiscount", "3", "Demo Discount 3",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), null, _clock.Now.AddDays(1)),
                new(null, "DemoDiscount", "4", "Demo Discount 4",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), _clock.Now.AddDays(-1),
                    _clock.Now.AddDays(1)),
                // These should not take effect:
                new(null, "DemoDiscount", "5", "Demo Discount 5",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), null, _clock.Now.AddDays(-1)),
                new(null, "DemoDiscount", "6", "Demo Discount 6",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), _clock.Now.AddDays(1), null),
                new(null, "DemoDiscount", "7", "Demo Discount 7",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), _clock.Now.AddDays(1),
                    _clock.Now.AddDays(2)),
                // Only the one with the highest discount amount should take effect: 
                new("A", "DemoDiscount", "8", "Demo Discount 8",
                    new DynamicDiscountAmountModel("USD", 0.10m, 0m, null), null, null),
                new("A", "DemoDiscount", "9", "Demo Discount 9",
                    new DynamicDiscountAmountModel("USD", 0.01m, 0m, null), null, null),
            };

            foreach (var candidate in candidates)
            {
                model.CandidateProductDiscounts.Add(candidate);
            }

            var orderDiscountPreviewInfoModels = new List<OrderDiscountPreviewInfoModel>
            {
                new(null, "DemoDiscount", "1", "Demo Discount 1", null, null, null),
                new(null, "DemoDiscount", "2", "Demo Discount 2", _clock.Now.AddDays(-1), _clock.Now.AddDays(1), null),
            };

            foreach (var preview in orderDiscountPreviewInfoModels)
            {
                model.OrderDiscountPreviews.Add(preview);
            }
        }

        return Task.CompletedTask;
    }
}