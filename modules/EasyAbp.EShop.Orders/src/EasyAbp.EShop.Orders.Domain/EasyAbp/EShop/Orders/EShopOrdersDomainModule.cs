using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Orders
{
    [DependsOn(
        typeof(AbpMapperlyModule),
        typeof(AbpBackgroundJobsAbstractionsModule),
        typeof(EShopPaymentsDomainSharedModule),
        typeof(EShopProductsDomainSharedModule),
        typeof(EShopOrdersDomainSharedModule)
    )]
    public class EShopOrdersDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapperlyObjectMapper<EShopOrdersDomainModule>();

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.EtoMappings.Add<Order, OrderEto>(typeof(EShopOrdersDomainModule));
                
                options.AutoEventSelectors.Add<Order>();
            });
        }
    }
}
