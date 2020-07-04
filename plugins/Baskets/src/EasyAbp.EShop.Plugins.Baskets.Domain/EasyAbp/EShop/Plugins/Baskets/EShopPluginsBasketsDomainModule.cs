using EasyAbp.EShop.Products;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsDomainSharedModule),
        typeof(EShopProductsDomainSharedModule)
    )]
    public class EShopPluginsBasketsDomainModule : AbpModule
    {

    }
}
