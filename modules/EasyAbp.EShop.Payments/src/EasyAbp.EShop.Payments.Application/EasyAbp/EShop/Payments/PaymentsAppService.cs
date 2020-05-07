using EasyAbp.EShop.Payments.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments
{
    public abstract class PaymentsAppService : ApplicationService
    {
        protected PaymentsAppService()
        {
            LocalizationResource = typeof(PaymentsResource);
            ObjectMapperContext = typeof(EShopPaymentsApplicationModule);
        }
    }
}
