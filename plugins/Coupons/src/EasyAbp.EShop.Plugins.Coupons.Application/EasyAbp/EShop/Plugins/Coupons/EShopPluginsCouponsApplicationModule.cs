using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopPluginsCouponsDomainModule),
        typeof(EShopPluginsCouponsApplicationContractsModule),
        typeof(EShopStoresApplicationSharedModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpMapperlyModule)
        )]
    public class EShopPluginsCouponsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<EShopPluginsCouponsApplicationModule>();
        }
    }
}
