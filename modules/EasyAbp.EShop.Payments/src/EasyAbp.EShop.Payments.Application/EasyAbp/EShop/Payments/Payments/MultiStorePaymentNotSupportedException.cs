using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class MultiStorePaymentNotSupportedException : BusinessException
    {
        public MultiStorePaymentNotSupportedException() : base(message: $"Should create payments for each store.")
        {
            
        }
    }
}