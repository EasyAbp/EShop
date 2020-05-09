using System;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Payments.WeChatPay.Settings
{
    public class WeChatPaySettingDefinitionProvider : SettingDefinitionProvider
    {
        private readonly IConfiguration _configuration;

        public WeChatPaySettingDefinitionProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from WeChatPaySettings class.
             */
            
            context.Add(
                new SettingDefinition(WeChatPaySettings.WeChatPayPaymentMethod.MchId),
                new SettingDefinition(WeChatPaySettings.WeChatPayPaymentMethod.ApiKey),
                new SettingDefinition(WeChatPaySettings.WeChatPayPaymentMethod.IsSandBox, "false"),
                new SettingDefinition(WeChatPaySettings.WeChatPayPaymentMethod.NotifyUrl,
                    _configuration["App:SelfUrl"].EnsureEndsWith('/') + "WeChatPay/Notify"),
                new SettingDefinition(WeChatPaySettings.WeChatPayPaymentMethod.RefundNotifyUrl,
                    _configuration["App:SelfUrl"].EnsureEndsWith('/') + "WeChatPay/RefundNotify"),
                new SettingDefinition(WeChatPaySettings.WeChatPayPaymentMethod.CertificatePath),
                new SettingDefinition(WeChatPaySettings.WeChatPayPaymentMethod.CertificateSecret)
            );
        }
    }
}