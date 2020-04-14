using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyMall.Data
{
    /* This is used if database provider does't define
     * IEasyMallDbSchemaMigrator implementation.
     */
    public class NullEasyMallDbSchemaMigrator : IEasyMallDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}