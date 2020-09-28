using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopPluginsCouponsApplicationModule),
        typeof(CouponsDomainTestModule)
        )]
    public class CouponsApplicationTestModule : AbpModule
    {

    }
}
