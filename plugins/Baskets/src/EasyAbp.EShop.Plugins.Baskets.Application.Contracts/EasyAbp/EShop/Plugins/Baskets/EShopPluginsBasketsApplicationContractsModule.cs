using EasyAbp.EShop.Products;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsDomainSharedModule),
        typeof(EShopProductsApplicationContractsModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EShopPluginsBasketsApplicationContractsModule : AbpModule
    {

    }
}
