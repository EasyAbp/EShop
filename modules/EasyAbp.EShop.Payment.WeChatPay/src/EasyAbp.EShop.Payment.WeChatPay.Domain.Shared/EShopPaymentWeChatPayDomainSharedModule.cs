using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.EShop.Payment.WeChatPay.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class EShopPaymentWeChatPayDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPaymentWeChatPayDomainSharedModule>("EasyAbp.EShop.Payment.WeChatPay");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<WeChatPayResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/WeChatPay");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("WeChatPay", typeof(WeChatPayResource));
            });
        }
    }
}
