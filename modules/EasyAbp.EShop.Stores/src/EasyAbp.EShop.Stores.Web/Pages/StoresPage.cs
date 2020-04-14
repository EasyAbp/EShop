using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Stores.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Stores.Web.Pages.StoresPage
     */
    public abstract class StoresPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<StoresResource> L { get; set; }
    }
}
