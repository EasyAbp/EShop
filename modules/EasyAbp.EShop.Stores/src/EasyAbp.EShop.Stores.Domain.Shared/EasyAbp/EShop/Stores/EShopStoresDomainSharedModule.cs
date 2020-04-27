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
                options.FileSets.AddEmbedded<EShopStoresDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<StoresResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/EasyAbp/EShop/Stores/Localization/Stores");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop.Stores", typeof(StoresResource));
            });
        }
    }
}
