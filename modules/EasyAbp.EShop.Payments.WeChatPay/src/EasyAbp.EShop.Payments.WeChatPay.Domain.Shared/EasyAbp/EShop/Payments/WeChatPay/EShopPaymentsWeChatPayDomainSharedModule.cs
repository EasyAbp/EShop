using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Payments.WeChatPay.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class EShopPaymentsWeChatPayDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPaymentsWeChatPayDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<WeChatPayResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/EasyAbp/EShop/Payments/WeChatPay/Localization/WeChatPay");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EasyAbp.EShop.Payments.WeChatPay", typeof(WeChatPayResource));
            });
        }
    }
}
