using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using EasyAbp.EShop.Payments.WeChatPay.Settings;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public class SettingOptionResolveContributor : IWeChatPayOptionResolveContributor
    {
        public const string ContributorName = "Setting";

        public string Name => ContributorName;

        public virtual async Task ResolveAsync(WeChatPayOptionsResolverContext context)
        {
            var settingProvider = context.ServiceProvider.GetRequiredService<ISettingProvider>();
            context.Options = new AbpWeChatPayOptions
            {
                ApiKey = await settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod.ApiKey),
                MchId = await settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod.MchId),
                IsSandBox = Convert.ToBoolean(await settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod.IsSandBox)),
                NotifyUrl = await settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod.NotifyUrl),
                RefundNotifyUrl = await settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod.RefundNotifyUrl),
                CertificatePath = await settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod.CertificatePath),
                CertificateSecret = await settingProvider.GetOrNullAsync(WeChatPaySettings.WeChatPayPaymentMethod.CertificateSecret)
            };
        }
    }
}