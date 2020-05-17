using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Payments.Localization;
using EasyAbp.PaymentService;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(PaymentServiceDomainSharedModule)
    )]
    public class EShopPaymentsDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPaymentsDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<PaymentsResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/EasyAbp/EShop/Payments/Localization/Payments");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop.Payments", typeof(PaymentsResource));
            });
        }
    }
}
