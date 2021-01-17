using EasyAbp.Abp.Trees;
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
        typeof(AbpTreesDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
    )]
    public class EShopProductsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopProductsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<ProductsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/EasyAbp/EShop/Products/Localization/Products");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop.Products", typeof(ProductsResource));
            });
        }
    }
}
