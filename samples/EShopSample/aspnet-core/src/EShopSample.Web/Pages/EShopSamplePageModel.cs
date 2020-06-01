using EShopSample.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EShopSample.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class EShopSamplePageModel : AbpPageModel
    {
        protected EShopSamplePageModel()
        {
            LocalizationResourceType = typeof(EShopSampleResource);
        }
    }
}