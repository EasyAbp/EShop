using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class DuplicateOrderExtraFeeException : BusinessException
    {
        public DuplicateOrderExtraFeeException(string extraFeeName, string extraFeeKey) : base(OrdersErrorCodes.DuplicateOrderExtraFee)
        {
            WithData(nameof(extraFeeName), extraFeeName);
            WithData(nameof(extraFeeKey), extraFeeKey);
        }
    }
}