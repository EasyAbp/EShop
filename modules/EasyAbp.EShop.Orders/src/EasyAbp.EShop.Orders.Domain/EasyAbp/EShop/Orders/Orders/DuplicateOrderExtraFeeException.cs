using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class DuplicateOrderExtraFeeException : BusinessException
    {
        public DuplicateOrderExtraFeeException(string extraFeeName, string extraFeeKey)
            : base("DuplicateOrderExtraFee", $"The extra fee {extraFeeName} (key: {extraFeeKey}) is existed.")
        {
        }
    }
}