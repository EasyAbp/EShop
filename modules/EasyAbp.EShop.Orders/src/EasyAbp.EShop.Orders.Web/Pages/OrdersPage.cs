using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using EasyAbp.EShop.Orders.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Orders.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits EasyAbp.EShop.Orders.Web.Pages.EShop.Orders.OrdersPage
     */
    public abstract class OrdersPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<OrdersResource> L { get; set; }
    }
}
