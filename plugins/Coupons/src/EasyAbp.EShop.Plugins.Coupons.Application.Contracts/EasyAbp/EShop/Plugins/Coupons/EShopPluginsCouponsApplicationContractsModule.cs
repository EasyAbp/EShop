using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopPluginsCouponsDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EShopPluginsCouponsApplicationContractsModule : AbpModule
    {

    }
}
