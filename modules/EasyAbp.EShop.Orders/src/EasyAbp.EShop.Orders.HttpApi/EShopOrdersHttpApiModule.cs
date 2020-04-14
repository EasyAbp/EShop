using Localization.Resources.AbpUi;
using EasyAbp.EShop.Orders.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(EShopOrdersApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopOrdersHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopOrdersHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<OrdersResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
