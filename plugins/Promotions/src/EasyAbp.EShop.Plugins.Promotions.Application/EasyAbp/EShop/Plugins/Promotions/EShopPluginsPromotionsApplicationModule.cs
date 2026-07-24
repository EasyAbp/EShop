using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(EShopPluginsPromotionsDomainModule),
    typeof(EShopPluginsPromotionsApplicationContractsModule),
    typeof(EShopStoresApplicationSharedModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpMapperlyModule)
)]
public class EShopPluginsPromotionsApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<EShopPluginsPromotionsApplicationModule>();
    }
}