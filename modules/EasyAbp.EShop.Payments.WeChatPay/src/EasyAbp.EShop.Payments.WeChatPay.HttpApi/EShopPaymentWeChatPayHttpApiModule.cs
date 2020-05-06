using Localization.Resources.AbpUi;
using EasyAbp.EShop.Payments.WeChatPay.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopPaymentsWeChatPayHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPaymentsWeChatPayHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<WeChatPayResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
