using Localization.Resources.AbpUi;
using EasyAbp.EShop.Plugins.Baskets.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Plugins.Baskets
{
    [DependsOn(
        typeof(EShopPluginsBasketsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopPluginsBasketsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPluginsBasketsHttpApiModule).Assembly);
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
