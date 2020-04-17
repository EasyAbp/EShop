using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Payment.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentWeChatPayDomainModule),
        typeof(EShopPaymentWeChatPayApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class EShopPaymentWeChatPayApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopPaymentWeChatPayApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopPaymentWeChatPayApplicationModule>(validate: true);
            });
        }
    }
}
