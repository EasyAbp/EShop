using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Stores.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class StoresPageModel : AbpPageModel
    {
        protected StoresPageModel()
        {
            LocalizationResourceType = typeof(StoresResource);
            ObjectMapperContext = typeof(EShopStoresWebModule);
        }
    }
}