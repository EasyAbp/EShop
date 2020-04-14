using Localization.Resources.AbpUi;
using EasyAbp.EShop.Payment.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Payment
{
    [DependsOn(
        typeof(EShopPaymentApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopPaymentHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPaymentHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<PaymentResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
