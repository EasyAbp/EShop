using EasyAbp.BookingService;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
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
    typeof(AbpAutoMapperModule)
    )]
public class EShopPluginsBookingApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EShopPluginsBookingApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EShopPluginsBookingApplicationModule>(validate: true);
        });
    }
}
