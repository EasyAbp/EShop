using Localization.Resources.AbpUi;
using EasyAbp.EShop.Plugins.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Plugins
{
    [DependsOn(
        typeof(EShopPluginsApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopPluginsHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPluginsHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<PluginsResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
