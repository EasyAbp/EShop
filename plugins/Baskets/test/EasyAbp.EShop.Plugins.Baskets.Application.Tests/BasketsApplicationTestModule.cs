using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(BasketsApplicationModule),
        typeof(BasketsDomainTestModule)
        )]
    public class BasketsApplicationTestModule : AbpModule
    {

    }
}
