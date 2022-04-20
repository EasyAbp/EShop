using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class OrderLineNotFoundException : BusinessException
    {
        public OrderLineNotFoundException(Guid orderId, Guid orderLineId) : base(PaymentsErrorCodes.OrderLineNotFound)
        {
            WithData(nameof(orderId), orderId);
            WithData(nameof(orderLineId), orderLineId);
        }
    }
}