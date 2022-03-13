using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class InvalidPaymentException : BusinessException
    {
        public InvalidPaymentException(Guid paymentId, Guid orderId) : base(OrdersErrorCodes.InvalidPayment)
        {
            WithData(nameof(paymentId), paymentId);
            WithData(nameof(orderId), orderId);
        }
    }
}