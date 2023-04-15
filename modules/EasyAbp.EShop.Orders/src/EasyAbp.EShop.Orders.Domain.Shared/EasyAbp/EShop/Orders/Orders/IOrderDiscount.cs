using System;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Orders.Orders;

public interface IOrderDiscount : IDiscountInfo
{
    Guid OrderId { get; }

    Guid OrderLineId { get; }

    decimal DiscountedAmount { get; }
}