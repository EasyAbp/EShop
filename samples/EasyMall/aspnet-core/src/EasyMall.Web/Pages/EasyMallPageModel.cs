using EasyMall.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyMall.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class EasyMallPageModel : AbpPageModel
    {
        protected EasyMallPageModel()
        {
            LocalizationResourceType = typeof(EasyMallResource);
        }
    }
}