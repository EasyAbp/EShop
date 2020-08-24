using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class MultiCurrencyNotSupportedException : BusinessException
    {
        public MultiCurrencyNotSupportedException() : base(message: $"The currency of all orders should be the same.")
        {
            
        }
    }
}