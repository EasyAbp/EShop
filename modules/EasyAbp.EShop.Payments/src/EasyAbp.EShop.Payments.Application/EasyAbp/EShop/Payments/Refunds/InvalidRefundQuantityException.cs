using Volo.Abp;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class InvalidRefundQuantityException : BusinessException
    {
        public InvalidRefundQuantityException(int quantity)
            : base("InvalidRefundQuantity", $"The refund quantity ({quantity}) is invalid.")
        {
        }
    }
}