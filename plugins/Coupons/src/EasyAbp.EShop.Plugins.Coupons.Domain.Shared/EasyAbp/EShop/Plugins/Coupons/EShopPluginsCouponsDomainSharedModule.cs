using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Plugins.Coupons.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class EShopPluginsCouponsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPluginsCouponsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<CouponsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("EasyAbp/EShop/Plugins/Coupons/Localization");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop.Plugins.Coupons", typeof(CouponsResource));
            });
        }
    }
}
