using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class InvalidOrderExtraFeeException : BusinessException
    {
        public InvalidOrderExtraFeeException(decimal extraFee) : base(OrdersErrorCodes.InvalidOrderExtraFee)
        {
            WithData(nameof(extraFee), extraFee);
        }
    }
}