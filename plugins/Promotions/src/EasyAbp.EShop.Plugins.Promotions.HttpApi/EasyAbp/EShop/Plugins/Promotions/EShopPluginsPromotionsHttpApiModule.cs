using Localization.Resources.AbpUi;
using EasyAbp.EShop.Plugins.Promotions.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Plugins.Promotions;

[DependsOn(
    typeof(EShopPluginsPromotionsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class EShopPluginsPromotionsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPluginsPromotionsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<PromotionsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
