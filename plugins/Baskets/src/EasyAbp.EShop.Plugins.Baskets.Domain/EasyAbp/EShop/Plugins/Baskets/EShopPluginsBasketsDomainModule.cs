using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsDomainSharedModule)
        )]
    public class EShopPluginsBasketsDomainModule : AbpModule
    {

    }
}
