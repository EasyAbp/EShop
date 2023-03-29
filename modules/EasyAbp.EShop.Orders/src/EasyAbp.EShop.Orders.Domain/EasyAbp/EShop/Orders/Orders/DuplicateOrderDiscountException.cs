using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class DuplicateOrderDiscountException : BusinessException
    {
        public DuplicateOrderDiscountException(Guid orderLineId, string discountName, string discountKey) : base(
            OrdersErrorCodes.DuplicateOrderDiscount)
        {
            WithData(nameof(orderLineId), orderLineId);
            WithData(nameof(discountName), discountName);
            WithData(nameof(discountKey), discountKey);
        }
    }
}