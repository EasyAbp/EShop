using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(AbpValidationModule)
)]
public class EShopPluginsFlashSalesDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsFlashSalesDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<FlashSalesResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("EasyAbp/EShop/Plugins/FlashSales/Localization");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace(FlashSalesErrorCodes.Namespace, typeof(FlashSalesResource));
        });
    }
}
