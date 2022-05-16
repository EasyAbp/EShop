using EasyAbp.EShop.Plugins.Booking.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking;

[Area(EShopPluginsBookingRemoteServiceConsts.ModuleName)]
public abstract class BookingController : AbpControllerBase
{
    protected BookingController()
    {
        LocalizationResource = typeof(BookingResource);
    }
}
