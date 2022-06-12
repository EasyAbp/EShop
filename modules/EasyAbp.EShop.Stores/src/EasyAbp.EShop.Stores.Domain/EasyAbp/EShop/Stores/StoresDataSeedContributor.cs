using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Stores
{
    public class StoresDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IStoreDataSeeder _storeDataSeeder;

        public StoresDataSeedContributor(IStoreDataSeeder storeDataSeeder)
        {
            _storeDataSeeder = storeDataSeeder;
        }
        
        [UnitOfWork]
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await _storeDataSeeder.SeedAsync(context);
        }
    }
}