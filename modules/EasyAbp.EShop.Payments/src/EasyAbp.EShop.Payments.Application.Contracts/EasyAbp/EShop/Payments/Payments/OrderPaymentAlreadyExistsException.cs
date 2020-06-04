using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class OrderPaymentAlreadyExistsException : BusinessException
    {
        public OrderPaymentAlreadyExistsException(Guid orderId)
            : base(message: $"The order {orderId}'s payment is already exists.")
        {
        }
    }
}