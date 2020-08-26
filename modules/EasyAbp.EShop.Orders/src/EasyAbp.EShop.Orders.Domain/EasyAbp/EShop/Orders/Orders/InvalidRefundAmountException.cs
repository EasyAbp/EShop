using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class InvalidRefundAmountException : BusinessException
    {
        public InvalidRefundAmountException(decimal amount)
            : base("InvalidRefundAmount", $"The refund amount ({amount}) is invalid.")
        {
        }
    }
}