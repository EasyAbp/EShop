using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Plugins.StoreApproval.MongoDB
{
    [DependsOn(
        typeof(StoreApprovalTestBaseModule),
        typeof(StoreApprovalMongoDbModule)
        )]
    public class StoreApprovalMongoDbTestModule : AbpModule
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