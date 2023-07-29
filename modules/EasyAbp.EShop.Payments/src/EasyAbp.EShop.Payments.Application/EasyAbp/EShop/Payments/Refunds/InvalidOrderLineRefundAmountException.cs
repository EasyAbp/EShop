using System;
using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class InvalidOrderLineRefundAmountException : BusinessException
    {
        public InvalidOrderLineRefundAmountException(Guid paymentId, Guid orderId, Guid orderLineId,
            decimal refundAmount) : base(PaymentsErrorCodes.InvalidOrderLineRefundAmount)
        {
            WithData(nameof(paymentId), paymentId);
            WithData(nameof(orderId), orderId);
            WithData(nameof(orderLineId), orderLineId);
            WithData(nameof(refundAmount), refundAmount);
        }
    }
}