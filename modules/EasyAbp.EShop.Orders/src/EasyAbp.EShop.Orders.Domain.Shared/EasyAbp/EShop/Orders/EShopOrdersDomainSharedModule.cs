using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Orders.Localization;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Stores;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(EShopStoresDomainSharedModule),
        typeof(EShopPaymentsDomainSharedModule)
    )]
    public class EShopOrdersDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopOrdersDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<OrdersResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("EasyAbp/EShop/Orders/Localization/Orders");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop.Orders", typeof(OrdersResource));
            });
        }
    }
}
