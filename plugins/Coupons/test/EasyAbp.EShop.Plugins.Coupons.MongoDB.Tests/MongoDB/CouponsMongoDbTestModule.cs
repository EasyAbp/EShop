using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Coupons.MongoDB
{
    [DependsOn(
        typeof(CouponsTestBaseModule),
        typeof(EShopPluginsCouponsMongoDbModule)
        )]
    public class CouponsMongoDbTestModule : AbpModule
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