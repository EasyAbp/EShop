using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyMall.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyMall.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyMall.Web.Pages.EasyMallPage
     */
    public abstract class EasyMallPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<EasyMallResource> L { get; set; }
    }
}
