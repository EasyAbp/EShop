using EasyAbp.EShop.Payments.WeChatPay.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public abstract class WeChatPayAppService : ApplicationService
    {
        protected WeChatPayAppService()
        {
            LocalizationResource = typeof(WeChatPayResource);
            ObjectMapperContext = typeof(EShopPaymentsWeChatPayApplicationModule);
        }
    }
}
