using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Baskets
{
    [DependsOn(
        typeof(EShopBasketsApplicationModule),
        typeof(EShopBasketsDomainTestModule)
        )]
    public class EShopBasketsApplicationTestModule : AbpModule
    {

    }
}
