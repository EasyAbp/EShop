using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Payments.Localization;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(AbpValidationModule),
        typeof(AbpDddDomainSharedModule),
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
                    .AddBaseTypes(typeof(PaymentServiceResource))
                    .AddVirtualJson("/EasyAbp/EShop/Payments/Localization/Payments");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop.Payments", typeof(PaymentsResource));
            });
        }
    }
}
