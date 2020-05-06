using EasyAbp.EShop.Payments.WeChatPay.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Payments.WeChatPay.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class WeChatPayPageModel : AbpPageModel
    {
        protected WeChatPayPageModel()
        {
            LocalizationResourceType = typeof(WeChatPayResource);
            ObjectMapperContext = typeof(EShopPaymentsWeChatPayWebModule);
        }
    }
}