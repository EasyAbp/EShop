using EasyAbp.EShop.Payment.WeChatPay.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    public abstract class WeChatPayController : AbpController
    {
        protected WeChatPayController()
        {
            LocalizationResource = typeof(WeChatPayResource);
        }
    }
}
