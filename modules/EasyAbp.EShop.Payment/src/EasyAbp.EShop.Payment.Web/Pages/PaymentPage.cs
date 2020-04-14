using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Payment.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Payment.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Payment.Web.Pages.PaymentPage
     */
    public abstract class PaymentPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<PaymentResource> L { get; set; }
    }
}
