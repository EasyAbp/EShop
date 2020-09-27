using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders
{
    public class DiscountAmountOverflowException : BusinessException
    {
        public DiscountAmountOverflowException()
            : base("DiscountAmountOverflow", $"The discount amount overflow.")
        {
        }
    }
}