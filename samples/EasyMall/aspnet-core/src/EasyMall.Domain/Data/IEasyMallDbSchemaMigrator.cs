using System.Threading.Tasks;

namespace EasyMall.Data
{
    public interface IEasyMallDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
