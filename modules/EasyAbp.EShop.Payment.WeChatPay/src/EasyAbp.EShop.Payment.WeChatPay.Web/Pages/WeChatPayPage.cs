using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Payment.WeChatPay.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Payment.WeChatPay.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Payment.WeChatPay.Web.Pages.WeChatPayPage
     */
    public abstract class WeChatPayPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<WeChatPayResource> L { get; set; }
    }
}
