using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class InvalidOrderExtraFeeException : BusinessException
    {
        public InvalidOrderExtraFeeException(decimal extraFee)
            : base("InvalidExtraFee", $"The extra fee {extraFee} is invalid.")
        {
        }
    }
}