using EasyAbp.EShop.Plugins.Booking.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class BookingPageModel : AbpPageModel
{
    protected BookingPageModel()
    {
        LocalizationResourceType = typeof(BookingResource);
        ObjectMapperContext = typeof(BookingWebModule);
    }
}
