using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(EShopPluginsCouponsDomainSharedModule)
    )]
    public class EShopPluginsCouponsDomainModule : AbpModule
    {

    }
}
