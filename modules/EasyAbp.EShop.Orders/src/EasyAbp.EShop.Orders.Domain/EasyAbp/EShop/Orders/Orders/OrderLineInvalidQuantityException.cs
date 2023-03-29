using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderLineInvalidQuantityException : BusinessException
    {
        public OrderLineInvalidQuantityException(Guid productId, Guid? productSkuId, int quantity) : base(OrdersErrorCodes.OrderLineInvalidQuantity)
        {
            WithData(nameof(productId), productId);
            WithData(nameof(productSkuId), productSkuId);
            WithData(nameof(quantity), quantity);
        }
    }
}