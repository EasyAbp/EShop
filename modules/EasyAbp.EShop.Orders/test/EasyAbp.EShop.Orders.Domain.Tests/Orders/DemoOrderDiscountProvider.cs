using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Orders.Orders;

public class DemoOrderDiscountProvider : IOrderDiscountProvider
{
    public static int DemoOrderDiscountEffectOrder { get; set; } = 10000;

    public int EffectOrder => DemoOrderDiscountEffectOrder;

    public Task DiscountAsync(OrderDiscountContext context)
    {
        var firstOrderLine = context.Order.OrderLines.First();

        var models = new List<OrderDiscountInfoModel>
        {
            new(new List<Guid> { firstOrderLine.Id }, null, "DemoDiscount1", "1", "Demo Discount 1",
                new DynamicDiscountAmountModel("USD", 0.01m, 0m, null)),
            new(new List<Guid> { firstOrderLine.Id }, "A", "DemoDiscount2", "2", "Demo Discount 2",
                new DynamicDiscountAmountModel("USD", 0.1m, 0m, null)),
            new(new List<Guid> { firstOrderLine.Id }, "A", "DemoDiscount3", "3", "Demo Discount 3",
                new DynamicDiscountAmountModel("USD", 0.05m, 0m, null)),
        };

        foreach (var model in models)
        {
            context.CandidateDiscounts.Add(model);
        }

        return Task.CompletedTask;
    }
}