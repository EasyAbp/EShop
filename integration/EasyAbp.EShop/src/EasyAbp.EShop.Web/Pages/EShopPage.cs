using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Web.Pages.EShopPage
     */
    public abstract class EShopPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<EShopResource> L { get; set; }
    }
}
