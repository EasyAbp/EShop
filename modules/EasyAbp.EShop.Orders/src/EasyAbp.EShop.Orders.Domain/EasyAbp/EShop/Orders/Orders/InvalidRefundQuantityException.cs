using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class InvalidRefundQuantityException : BusinessException
    {
        public InvalidRefundQuantityException(int quantity) : base(OrdersErrorCodes.InvalidRefundQuantity)
        {
            WithData(nameof(quantity), quantity);
        }
    }
}