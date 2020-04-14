using EasyMall.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EasyMall.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(EasyMallEntityFrameworkCoreDbMigrationsModule),
        typeof(EasyMallApplicationContractsModule)
        )]
    public class EasyMallDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
