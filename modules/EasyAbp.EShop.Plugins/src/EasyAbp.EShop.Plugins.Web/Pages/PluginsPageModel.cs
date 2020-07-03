using EasyAbp.EShop.Plugins.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class PluginsPageModel : AbpPageModel
    {
        protected PluginsPageModel()
        {
            LocalizationResourceType = typeof(PluginsResource);
            ObjectMapperContext = typeof(EShopPluginsWebModule);
        }
    }
}