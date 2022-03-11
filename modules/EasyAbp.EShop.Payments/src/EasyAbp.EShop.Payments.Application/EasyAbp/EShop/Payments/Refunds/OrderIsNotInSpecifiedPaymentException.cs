using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class OrderIsNotInSpecifiedPaymentException : BusinessException
    {
        public OrderIsNotInSpecifiedPaymentException(Guid orderId, Guid paymentId) : base(PaymentsErrorCodes.OrderIsNotInSpecifiedPayment)
        {
            WithData(nameof(orderId), orderId);
            WithData(nameof(paymentId), paymentId);
        }
    }
}