using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Products.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Products.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Products.Web.Pages.ProductsPage
     */
    public abstract class ProductsPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<ProductsResource> L { get; set; }
    }
}
