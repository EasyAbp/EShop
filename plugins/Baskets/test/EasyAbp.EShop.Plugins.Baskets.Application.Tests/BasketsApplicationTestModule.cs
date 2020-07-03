using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsApplicationModule),
        typeof(BasketsDomainTestModule)
        )]
    public class BasketsApplicationTestModule : AbpModule
    {

    }
}
