using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Orders.Orders;

public class DemoOrderDiscountProvider : IOrderDiscountProvider
{
    public Task<List<OrderDiscountInfoModel>> GetAllAsync(Order order, Dictionary<Guid, IProduct> productDict)
    {
        return Task.FromResult(new List<OrderDiscountInfoModel>
        {
            new(order.OrderLines.First().Id, "DemoDiscount1", "1", "Demo Discount 1", 0.01m),
            new(order.OrderLines.First().Id, "DemoDiscount2", "2", "Demo Discount 2", 0.1m),
        });
    }
}