using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.Baskets.MongoDB
{
    [DependsOn(
        typeof(BasketsTestBaseModule),
        typeof(BasketsMongoDbModule)
        )]
    public class BasketsMongoDbTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var connectionString = MongoDbFixture.ConnectionString.EnsureEndsWith('/') +
                                   "Db_" +
                                    Guid.NewGuid().ToString("N");

            Configure<AbpDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = connectionString;
            });
        }
    }
}