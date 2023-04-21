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
        var orderLines = context.Order.OrderLines.ToList();
        var firstOrderLine = orderLines[0];
        var secondOrderLine = orderLines[1];

        var models = new List<OrderDiscountInfoModel>
        {
            new(new List<Guid> { firstOrderLine.Id }, null, "DemoDiscount1", "1", "Demo Discount 1",
                new DynamicDiscountAmountModel("USD", 5.00m, 0m, null)),
            new(new List<Guid> { secondOrderLine.Id }, "A", "DemoDiscount2", "2", "Demo Discount 2",
                new DynamicDiscountAmountModel("USD", 0.10m, 0m, null)),
            new(new List<Guid> { secondOrderLine.Id }, "A", "DemoDiscount3", "3", "Demo Discount 3",
                new DynamicDiscountAmountModel("USD", 0.05m, 0m, null)),
            new(new List<Guid> { firstOrderLine.Id, secondOrderLine.Id }, "B", "DemoDiscount4", "4", "Demo Discount 4",
                new DynamicDiscountAmountModel("USD", 0.01m, 0m, null)),
            new(new List<Guid> { firstOrderLine.Id, secondOrderLine.Id }, null, "DemoDiscount5", "5", "Demo Discount 5",
                new DynamicDiscountAmountModel("USD", 0.01m, 0m, null)),
            new(new List<Guid> { firstOrderLine.Id }, "B", "DemoDiscount6", "6", "Demo Discount 6",
                new DynamicDiscountAmountModel("USD", 0.00m, 0.55m, null)),
        };

        foreach (var model in models)
        {
            context.CandidateDiscounts.Add(model);
        }

        return Task.CompletedTask;
    }
}