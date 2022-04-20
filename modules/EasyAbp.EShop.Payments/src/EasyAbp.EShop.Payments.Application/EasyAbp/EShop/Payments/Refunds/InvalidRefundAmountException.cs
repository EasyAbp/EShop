using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class InvalidRefundAmountException : BusinessException
    {
        public InvalidRefundAmountException(Guid paymentId, Guid paymentItemId, decimal refundAmount) : base(
            PaymentsErrorCodes.InvalidRefundAmount)
        {
            WithData(nameof(paymentId), paymentId);
            WithData(nameof(paymentItemId), paymentItemId);
            WithData(nameof(refundAmount), refundAmount);
        }
    }
}