using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class MultiStorePaymentNotSupportedException : BusinessException
    {
        public MultiStorePaymentNotSupportedException() : base(PaymentsErrorCodes.MultiStorePaymentNotSupported)
        {
        }
    }
}