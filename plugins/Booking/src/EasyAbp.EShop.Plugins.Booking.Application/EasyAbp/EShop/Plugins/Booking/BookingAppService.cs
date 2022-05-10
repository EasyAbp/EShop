using EasyAbp.EShop.Plugins.Booking.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking;

public abstract class BookingAppService : ApplicationService
{
    protected BookingAppService()
    {
        LocalizationResource = typeof(BookingResource);
        ObjectMapperContext = typeof(EShopPluginsBookingApplicationModule);
    }
}
