using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class FlashSalesPageModel : AbpPageModel
{
    protected FlashSalesPageModel()
    {
        LocalizationResourceType = typeof(FlashSalesResource);
        ObjectMapperContext = typeof(FlashSalesWebModule);
    }
}
