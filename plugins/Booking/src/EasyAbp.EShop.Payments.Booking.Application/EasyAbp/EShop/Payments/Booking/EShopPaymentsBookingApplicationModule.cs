using EasyAbp.BookingService;
using EasyAbp.EShop.Payments.Booking.Authorization;
using EasyAbp.EShop.Plugins.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.Booking
{
    [DependsOn(
        typeof(EShopPaymentsApplicationModule),
        typeof(EShopPluginsBookingApplicationContractsModule),
        typeof(BookingServiceApplicationContractsModule)
    )]
    public class EShopPaymentsBookingApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IAuthorizationHandler, BookingPaymentCreationAuthorizationHandler>();
        }
    }
}
