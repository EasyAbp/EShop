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

    public Task DiscountAsync(ProductDiscountContext context)
    {
        if (context.Product.Id != ProductsTestData.Product1Id ||
            context.ProductSku.Id != ProductsTestData.Product1Sku1Id)
        {
            return Task.CompletedTask;
        }

        context.PriceDataModel.ProductDiscounts.AddRange(new List<ProductDiscountInfoModel>
        {
            // These should affect:
            new("DemoDiscount", "1", "Demo Discount 1", 0.01m, null, null),
            new("DemoDiscount", "2", "Demo Discount 2", 0.01m, _clock.Now.AddDays(-1), null),
            new("DemoDiscount", "3", "Demo Discount 3", 0.01m, null, _clock.Now.AddDays(1)),
            new("DemoDiscount", "4", "Demo Discount 4", 0.01m, _clock.Now.AddDays(-1), _clock.Now.AddDays(1)),
            // These should not affect: 0.01m,
            new("DemoDiscount", "5", "Demo Discount 5", 0.01m, null, _clock.Now.AddDays(-1)),
            new("DemoDiscount", "6", "Demo Discount 6", 0.01m, _clock.Now.AddDays(1), null),
            new("DemoDiscount", "7", "Demo Discount 7", 0.01m, _clock.Now.AddDays(1), _clock.Now.AddDays(2)),
        });

        context.PriceDataModel.OrderDiscountPreviews.AddRange(new List<OrderDiscountPreviewInfoModel>
        {
            new("DemoDiscount", "1", "Demo Discount 1", 0.01m, 0.01m, null, null),
            new("DemoDiscount", "2", "Demo Discount 2", 0.01m, 0.01m, _clock.Now.AddDays(-1), _clock.Now.AddDays(1)),
        });

        return Task.CompletedTask;
    }
}