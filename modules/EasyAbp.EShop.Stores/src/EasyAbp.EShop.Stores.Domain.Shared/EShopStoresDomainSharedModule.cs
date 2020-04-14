using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Stores.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Stores
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class EShopStoresDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopStoresDomainSharedModule>("EasyAbp.EShop.Stores");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<StoresResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Stores");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Stores", typeof(StoresResource));
            });
        }
    }
}
