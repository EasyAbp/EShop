using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Baskets.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Baskets
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class EShopBasketsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopBasketsDomainSharedModule>("EasyAbp.EShop.Baskets");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BasketsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Baskets");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Baskets", typeof(BasketsResource));
            });
        }
    }
}
