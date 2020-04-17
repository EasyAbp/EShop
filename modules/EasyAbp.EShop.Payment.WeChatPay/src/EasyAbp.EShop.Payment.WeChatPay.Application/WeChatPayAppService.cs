using EasyAbp.EShop.Payment.WeChatPay.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    public abstract class WeChatPayAppService : ApplicationService
    {
        protected WeChatPayAppService()
        {
            LocalizationResource = typeof(WeChatPayResource);
            ObjectMapperContext = typeof(EShopPaymentWeChatPayApplicationModule);
        }
    }
}
