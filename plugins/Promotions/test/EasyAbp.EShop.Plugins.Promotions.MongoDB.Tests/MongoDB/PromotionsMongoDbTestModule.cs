using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.Promotions.MongoDB;

[DependsOn(
    typeof(PromotionsTestBaseModule),
    typeof(EShopPluginsPromotionsMongoDbModule)
    )]
public class PromotionsMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
