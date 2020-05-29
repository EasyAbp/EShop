using EasyAbp.EShop.Baskets;
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Localization;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(EShopBasketsDomainSharedModule),
        typeof(EShopOrdersDomainSharedModule),
        typeof(EShopPaymentsDomainSharedModule),
        typeof(EShopProductsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule)
    )]
    public class EShopDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<EShopResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/EasyAbp/EShop/Localization/EShop");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop", typeof(EShopResource));
            });
        }
    }
}
