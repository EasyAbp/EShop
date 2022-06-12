using Localization.Resources.AbpUi;
using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(FlashSalesApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class FlashSalesHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(FlashSalesHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<FlashSalesResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
