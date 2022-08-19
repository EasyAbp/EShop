using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

[DependsOn(
    typeof(EShopPluginsFlashSalesDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class EShopPluginsFlashSalesMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<FlashSalesMongoDbContext>(options =>
        {
            options.AddRepository<FlashSalePlan, MongoFlashSalePlanRepository>();
            options.AddRepository<FlashSaleResult, MongoFlashSaleResultRepository>();
        });
    }
}
