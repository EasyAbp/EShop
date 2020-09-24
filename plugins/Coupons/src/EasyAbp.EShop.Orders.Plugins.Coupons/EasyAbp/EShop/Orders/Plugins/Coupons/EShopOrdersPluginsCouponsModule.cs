using EasyAbp.EShop.Orders.Plugins.Coupons.Authorization;
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
            context.Services.AddSingleton<IAuthorizationHandler, CouponOrderCreationAuthorizationHandler>();
        }
    }
}
