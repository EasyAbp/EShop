using EasyAbp.BookingService;
using EasyAbp.EShop.Orders;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(BookingServiceDomainSharedModule),
    typeof(EShopPluginsBookingDomainSharedModule),
    typeof(EShopOrdersApplicationContractsModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class EShopPluginsBookingApplicationContractsModule : AbpModule
{

}
