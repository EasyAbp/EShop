using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(BasketsDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class BasketsApplicationContractsModule : AbpModule
    {

    }
}
