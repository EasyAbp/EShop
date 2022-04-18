using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsDomainModule),
        typeof(EShopPaymentsApplicationContractsModule),
        typeof(EShopStoresApplicationSharedModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class EShopPaymentsApplicationModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<IAuthorizationHandler, BasicPaymentCreationAuthorizationHandler>();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<EShopPaymentsApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopPaymentsApplicationModule>(validate: true);
            });
        }
    }
}
