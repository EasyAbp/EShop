using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class InvalidOrderExtraFeeRefundAmountException : BusinessException
    {
        public InvalidOrderExtraFeeRefundAmountException(Guid paymentId, Guid orderId, string extraFeeDisplayName,
            decimal refundAmount) : base(PaymentsErrorCodes.InvalidOrderExtraFeeRefundAmount)
        {
            WithData(nameof(paymentId), paymentId);
            WithData(nameof(orderId), orderId);
            WithData(nameof(extraFeeDisplayName), extraFeeDisplayName);
            WithData(nameof(refundAmount), refundAmount);
        }
    }
}