using EasyAbp.EShop.Orders.Plugins.Coupons.Authorization;
using EasyAbp.EShop.Orders.Plugins.Coupons.ObjectExtending;
using EasyAbp.EShop.Plugins.Coupons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders.Plugins.Coupons
{
    [DependsOn(
        typeof(EShopOrdersDomainModule),
        typeof(EShopPluginsCouponsDomainSharedModule)
    )]
    public class EShopOrdersPluginsCouponsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopOrdersPluginsCouponsObjectExtensions.Configure();

            context.Services.AddSingleton<IAuthorizationHandler, CouponOrderCreationAuthorizationHandler>();
        }
    }
}
