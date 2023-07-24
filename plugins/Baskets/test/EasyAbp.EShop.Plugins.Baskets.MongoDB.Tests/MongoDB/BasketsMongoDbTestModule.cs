using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets.MongoDB
{
    [DependsOn(
        typeof(BasketsTestBaseModule),
        typeof(EShopPluginsBasketsMongoDbModule)
        )]
    public class BasketsMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
            });
        }
    }
}