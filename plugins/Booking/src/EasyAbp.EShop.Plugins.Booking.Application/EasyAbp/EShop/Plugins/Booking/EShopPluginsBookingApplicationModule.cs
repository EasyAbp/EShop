using EasyAbp.BookingService;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Plugins.Booking;

[DependsOn(
    typeof(EShopStoresApplicationSharedModule),
    typeof(EShopProductsApplicationContractsModule),
    typeof(EShopPluginsBookingDomainModule),
    typeof(EShopPluginsBookingApplicationContractsModule),
    typeof(BookingServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule)
    )]
public class EShopPluginsBookingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<EShopPluginsBookingApplicationModule>();
    }
}
