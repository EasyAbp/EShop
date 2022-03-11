using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class InvalidRefundQuantityException : BusinessException
    {
        public InvalidRefundQuantityException(int quantity) : base(PaymentsErrorCodes.InvalidRefundQuantity)
        {
            WithData(nameof(quantity), quantity);
        }
    }
}