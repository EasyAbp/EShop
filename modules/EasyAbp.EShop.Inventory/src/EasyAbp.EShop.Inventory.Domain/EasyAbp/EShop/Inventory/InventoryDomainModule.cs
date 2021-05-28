using EasyAbp.EShop.Inventory.Inventories;
using EasyAbp.EShop.Inventory.StockHistories;
using EasyAbp.EShop.Inventory.Stocks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(AbpDddDomainModule),
        typeof(InventoryDomainSharedModule)
    )]
    public class InventoryDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<InventoryDomainModule>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddProfile<InventoryDomainAutoMapperProfile>(validate: false);
            });

            Configure<AbpDistributedEntityEventOptions>(options =>
            {
                options.AutoEventSelectors.Add<Stock>();
                options.AutoEventSelectors.Add<StockHistory>();
                
            });
        }
    }
}
