using EasyAbp.EShop.Stores;
using EasyAbp.PaymentService;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(EShopPaymentsDomainSharedModule),
        typeof(EShopStoresDomainSharedModule),
        typeof(PaymentServiceDomainSharedModule)
        )]
    public class EShopPaymentsDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopPaymentsDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<PaymentsDomainAutoMapperProfile>(validate: true);
            });
        }
    }
}
