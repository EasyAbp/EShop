using EasyAbp.EShop.Plugins.Coupons.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class CouponsPageModel : AbpPageModel
    {
        protected CouponsPageModel()
        {
            LocalizationResourceType = typeof(CouponsResource);
            ObjectMapperContext = typeof(EShopPluginsCouponsWebModule);
        }
    }
}