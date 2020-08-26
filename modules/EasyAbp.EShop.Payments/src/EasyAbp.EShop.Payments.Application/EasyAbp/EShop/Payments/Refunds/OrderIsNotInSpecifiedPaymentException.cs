using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class OrderIsNotInSpecifiedPaymentException : BusinessException
    {
        public OrderIsNotInSpecifiedPaymentException(Guid orderId, Guid paymentId)
            : base("OrderIsNotInSpecifiedPayment", $"The order ({orderId}) is not in the specified payment ({paymentId}).")
        {
        }
    }
}