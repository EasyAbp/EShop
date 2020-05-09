using EasyAbp.EShop.Payments.WeChatPay.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public abstract class WeChatPayController : AbpController
    {
        protected WeChatPayController()
        {
            LocalizationResource = typeof(WeChatPayResource);
        }
    }
}
