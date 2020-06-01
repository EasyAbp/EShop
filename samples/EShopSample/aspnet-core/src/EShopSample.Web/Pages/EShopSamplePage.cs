using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EShopSample.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EShopSample.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EShopSample.Web.Pages.EShopSamplePage
     */
    public abstract class EShopSamplePage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<EShopSampleResource> L { get; set; }
    }
}
