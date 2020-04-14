using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Baskets.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Baskets.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Baskets.Web.Pages.BasketsPage
     */
    public abstract class BasketsPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<BasketsResource> L { get; set; }
    }
}
