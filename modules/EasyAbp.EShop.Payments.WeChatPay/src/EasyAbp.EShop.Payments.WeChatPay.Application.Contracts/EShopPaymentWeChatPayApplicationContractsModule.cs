using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    [DependsOn(
        typeof(EShopPaymentsWeChatPayDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class EShopPaymentsWeChatPayApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopPaymentsWeChatPayApplicationContractsModule>("EasyAbp.EShop.Payments.WeChatPay");
            });
        }
    }
}
