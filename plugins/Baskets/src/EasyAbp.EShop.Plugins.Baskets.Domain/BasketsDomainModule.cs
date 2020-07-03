using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(BasketsDomainSharedModule)
        )]
    public class BasketsDomainModule : AbpModule
    {

    }
}
