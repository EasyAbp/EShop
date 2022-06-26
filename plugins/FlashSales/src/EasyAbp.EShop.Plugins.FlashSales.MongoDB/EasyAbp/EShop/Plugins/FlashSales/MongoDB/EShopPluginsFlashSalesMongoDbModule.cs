using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
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
            options.AddRepository<FlashSalesPlan, MongoFlashSalesPlanRepository>();
            options.AddRepository<FlashSalesResult, MongoFlashSalesResultRepository>();
        });
    }
}
