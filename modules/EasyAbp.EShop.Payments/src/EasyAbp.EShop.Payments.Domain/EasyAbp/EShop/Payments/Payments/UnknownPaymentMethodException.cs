using Volo.Abp;

namespace EasyAbp.EShop.Payments.Payments
{
    public class UnknownPaymentMethodException : BusinessException
    {
        public UnknownPaymentMethodException(string paymentMethod) : base(
            message: $"Payment method {paymentMethod} does not exist.")
        {
        }
    }
}