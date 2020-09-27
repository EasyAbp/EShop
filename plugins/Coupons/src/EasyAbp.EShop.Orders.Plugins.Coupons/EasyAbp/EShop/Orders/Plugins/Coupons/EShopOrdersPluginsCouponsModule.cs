using EasyAbp.EShop.Orders.Plugins.Coupons.ObjectExtending;
using EasyAbp.EShop.Plugins.Coupons;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopOrdersDomainModule),
        typeof(EShopPluginsCouponsApplicationContractsModule)
    )]
    public class EShopOrdersPluginsCouponsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopOrdersPluginsCouponsObjectExtensions.Configure();
        }
    }
}
