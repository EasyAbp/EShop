using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(AbpAutoMapperModule),
    typeof(EShopPluginsFlashSalesDomainSharedModule)
)]
public class EShopPluginsFlashSalesDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<EShopPluginsFlashSalesDomainModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EShopPluginsFlashSalesDomainModule>(validate: true);
        });

        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.EtoMappings.Add<FlashSalePlan, FlashSalePlanEto>(typeof(EShopPluginsFlashSalesDomainModule));

            options.AutoEventSelectors.Add<FlashSalePlan>();
        });
    }
}
