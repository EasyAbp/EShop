using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(InventoryDomainModule),
        typeof(InventoryApplicationContractsModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpAutoMapperModule)
        )]
    public class InventoryApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<InventoryApplicationModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<InventoryApplicationModule>(validate: true);
            });
        }
    }
}
