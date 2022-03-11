using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class InvalidRefundAmountException : BusinessException
    {
        public InvalidRefundAmountException(decimal amount) : base(OrdersErrorCodes.InvalidRefundAmount)
        {
            WithData(nameof(amount), amount);
        }
    }
}