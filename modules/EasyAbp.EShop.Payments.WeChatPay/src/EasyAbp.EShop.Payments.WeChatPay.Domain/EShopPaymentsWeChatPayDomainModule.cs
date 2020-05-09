using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Pay;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayDomainSharedModule),
        typeof(AbpWeChatPayModule)
    )]
    public class EShopPaymentsWeChatPayDomainModule : AbpModule
    {
        public override void PostConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpWeChatPayResolveOptions>(options =>
            {
                options.ResolveContributors.AddFirst(new SettingOptionResolveContributor());
            });
        }
    }
}
