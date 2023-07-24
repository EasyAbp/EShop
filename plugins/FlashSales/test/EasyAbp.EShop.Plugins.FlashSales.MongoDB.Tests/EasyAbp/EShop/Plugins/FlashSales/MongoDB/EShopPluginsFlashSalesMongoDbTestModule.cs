using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.MongoDB;

[DependsOn(
    typeof(EShopPluginsFlashSalesTestBaseModule),
    typeof(EShopPluginsFlashSalesMongoDbModule)
    )]
public class EShopPluginsFlashSalesMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
