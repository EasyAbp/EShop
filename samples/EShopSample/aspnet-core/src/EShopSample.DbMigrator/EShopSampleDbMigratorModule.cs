using EShopSample.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EShopSample.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(EShopSampleEntityFrameworkCoreModule),
        typeof(EShopSampleApplicationContractsModule)
        )]
    public class EShopSampleDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
