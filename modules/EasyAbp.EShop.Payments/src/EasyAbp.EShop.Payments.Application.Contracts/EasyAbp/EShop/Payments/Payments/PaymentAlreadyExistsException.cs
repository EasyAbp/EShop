using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentAlreadyExistsException : BusinessException
    {
        public PaymentAlreadyExistsException(Guid orderId)
            : base(message: $"The order {orderId}'s payment is already exists.")
        {
        }
    }
}