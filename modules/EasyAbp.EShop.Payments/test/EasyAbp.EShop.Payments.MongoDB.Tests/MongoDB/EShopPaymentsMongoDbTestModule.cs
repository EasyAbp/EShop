using System;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments.MongoDB
{
    [DependsOn(
        typeof(EShopPaymentsTestBaseModule),
        typeof(EShopPaymentsMongoDbModule)
        )]
    public class EShopPaymentsMongoDbTestModule : AbpModule
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