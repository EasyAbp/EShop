using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Mapperly;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpMapperlyModule),
    typeof(EShopPluginsFlashSalesDomainSharedModule)
)]
public class EShopPluginsFlashSalesDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<EShopPluginsFlashSalesDomainModule>();


        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<FlashSalePlan, FlashSalePlanEto>(typeof(EShopPluginsFlashSalesDomainModule));

            options.AutoEventSelectors.Add<FlashSalePlan>();
        });
    }
}
