using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Settings;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreDataSeeder : IStoreDataSeeder, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly ISettingProvider _settingProvider;
        private readonly IStoreRepository _storeRepository;

        public StoreDataSeeder(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            ISettingProvider settingProvider,
            IStoreRepository storeRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _settingProvider = settingProvider;
            _storeRepository = storeRepository;
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context.TenantId))
            {
                if (await _storeRepository.GetCountAsync() == 0)
                {
                    await _storeRepository.InsertAsync(
                        new Store(_guidGenerator.Create(), _currentTenant.Id,
                            await _settingProvider.GetOrNullAsync(StoresSettings.DefaultStoreName)), true);
                }
            }
        }
    }
}