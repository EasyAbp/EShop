using System.Threading.Tasks;

namespace EShopSample.Data
{
    public interface IEShopSampleDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
