using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Payments.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Payments.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Payments.Web.Pages.PaymentsPage
     */
    public abstract class PaymentsPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<PaymentsResource> L { get; set; }
    }
}
