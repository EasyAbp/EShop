using Localization.Resources.AbpUi;
using EasyAbp.EShop.Payments.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopPaymentsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPaymentsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<PaymentsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
