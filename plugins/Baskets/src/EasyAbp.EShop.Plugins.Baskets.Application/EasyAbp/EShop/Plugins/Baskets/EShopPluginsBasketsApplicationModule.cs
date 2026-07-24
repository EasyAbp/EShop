using EasyAbp.EShop.Products;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsDomainModule),
        typeof(EShopPluginsBasketsApplicationContractsModule),
        typeof(EShopProductsApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpMapperlyModule)
    )]
    public class EShopPluginsBasketsApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<EShopPluginsBasketsApplicationModule>();
        }
    }
}
