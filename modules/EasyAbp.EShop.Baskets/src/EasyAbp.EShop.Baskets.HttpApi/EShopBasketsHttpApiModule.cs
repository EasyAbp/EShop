using Localization.Resources.AbpUi;
using EasyAbp.EShop.Baskets.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Baskets
{
    [DependsOn(
        typeof(EShopBasketsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopBasketsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopBasketsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<BasketsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
