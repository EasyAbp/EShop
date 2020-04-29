using System.Threading.Tasks;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Stores.Stores
{
    public interface IStoreDataSeeder
    {
        Task SeedAsync(DataSeedContext context);
    }
}