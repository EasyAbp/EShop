using EasyAbp.EShop.Plugins.StoreApproval.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.StoreApproval.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class StoreApprovalPageModel : AbpPageModel
    {
        protected StoreApprovalPageModel()
        {
            LocalizationResourceType = typeof(StoreApprovalResource);
            ObjectMapperContext = typeof(StoreApprovalWebModule);
        }
    }
}