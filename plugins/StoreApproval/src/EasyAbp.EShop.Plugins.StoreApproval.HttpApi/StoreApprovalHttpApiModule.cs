using Localization.Resources.AbpUi;
using EasyAbp.EShop.Plugins.StoreApproval.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Plugins.StoreApproval
{
    [DependsOn(
        typeof(StoreApprovalApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class StoreApprovalHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(StoreApprovalHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<StoreApprovalResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
