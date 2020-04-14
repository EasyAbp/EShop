using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Products.Localization;
using EasyAbp.EShop.Stores;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Products
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(EShopStoresDomainSharedModule)
    )]
    public class EShopProductsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopProductsDomainSharedModule>("EasyAbp.EShop.Products");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<ProductsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Products");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Products", typeof(ProductsResource));
            });
        }
    }
}
