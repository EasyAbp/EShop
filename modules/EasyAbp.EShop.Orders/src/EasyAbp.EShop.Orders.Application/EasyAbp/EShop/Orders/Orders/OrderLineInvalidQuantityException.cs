using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderLineInvalidQuantityException : BusinessException
    {
        public OrderLineInvalidQuantityException(Guid productId, Guid? productSkuId, int quantity) : base(
            message: $"Invalid quantity {quantity} for product {productId} (SKU: {productSkuId}).")
        {
        }
    }
}