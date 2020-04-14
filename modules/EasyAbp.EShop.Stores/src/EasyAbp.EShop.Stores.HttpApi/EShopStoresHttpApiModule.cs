using Localization.Resources.AbpUi;
using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(EShopStoresApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EShopStoresHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopStoresHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<StoresResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
