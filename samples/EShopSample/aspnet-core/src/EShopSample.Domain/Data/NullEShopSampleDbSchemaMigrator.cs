using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EShopSample.Data
{
    /* This is used if database provider does't define
     * IEShopSampleDbSchemaMigrator implementation.
     */
    public class NullEShopSampleDbSchemaMigrator : IEShopSampleDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}