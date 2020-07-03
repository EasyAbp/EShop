using EasyAbp.EShop.Plugins.Baskets.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class BasketsPageModel : AbpPageModel
    {
        protected BasketsPageModel()
        {
            LocalizationResourceType = typeof(BasketsResource);
            ObjectMapperContext = typeof(BasketsWebModule);
        }
    }
}