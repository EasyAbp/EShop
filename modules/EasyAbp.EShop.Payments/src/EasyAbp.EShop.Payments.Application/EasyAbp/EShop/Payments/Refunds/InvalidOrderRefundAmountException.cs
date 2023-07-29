using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class InvalidOrderRefundAmountException : BusinessException
    {
        public InvalidOrderRefundAmountException(Guid paymentId, Guid orderId, decimal refundAmount) : base(
            PaymentsErrorCodes.InvalidOrderRefundAmount)
        {
            WithData(nameof(paymentId), paymentId);
            WithData(nameof(orderId), orderId);
            WithData(nameof(refundAmount), refundAmount);
        }
    }
}