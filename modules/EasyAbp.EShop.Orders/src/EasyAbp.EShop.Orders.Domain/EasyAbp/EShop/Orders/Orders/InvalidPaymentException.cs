using System;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class InvalidPaymentException : BusinessException
    {
        public InvalidPaymentException(Guid paymentId, Guid orderId)
            : base(message: $"The payment {paymentId} has invalid configurations for the order {orderId}.")
        {
        }
    }
}