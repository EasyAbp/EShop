using EasyAbp.EShop.Plugins.Promotions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.Promotions.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PromotionsPageModel : AbpPageModel
{
    protected PromotionsPageModel()
    {
        LocalizationResourceType = typeof(PromotionsResource);
        ObjectMapperContext = typeof(EShopPluginsPromotionsWebModule);
    }
}
