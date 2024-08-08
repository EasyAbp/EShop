using EasyAbp.EShop.Orders.Plugins.Coupons;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopOrdersPluginsCouponsModule),
        typeof(CouponsApplicationTestModule),
        typeof(CouponsDomainTestModule)
        )]
    public class EShopOrdersPluginsCouponsTestModule : AbpModule
    {

    }
}
