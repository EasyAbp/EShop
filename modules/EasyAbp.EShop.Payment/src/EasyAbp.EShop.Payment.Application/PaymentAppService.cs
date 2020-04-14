using EasyAbp.EShop.Payment.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payment
{
    public abstract class PaymentAppService : ApplicationService
    {
        protected PaymentAppService()
        {
            LocalizationResource = typeof(PaymentResource);
            ObjectMapperContext = typeof(EShopPaymentApplicationModule);
        }
    }
}
