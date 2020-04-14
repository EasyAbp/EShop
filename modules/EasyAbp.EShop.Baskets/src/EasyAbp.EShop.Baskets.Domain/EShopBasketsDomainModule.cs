using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Baskets
{
    [DependsOn(
        typeof(EShopBasketsDomainSharedModule)
        )]
    public class EShopBasketsDomainModule : AbpModule
    {

    }
}
