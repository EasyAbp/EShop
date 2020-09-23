using EasyAbp.EShop.Stores;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(EShopPluginsCouponsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
    )]
    public class EShopPluginsCouponsDomainModule : AbpModule
    {

    }
}
