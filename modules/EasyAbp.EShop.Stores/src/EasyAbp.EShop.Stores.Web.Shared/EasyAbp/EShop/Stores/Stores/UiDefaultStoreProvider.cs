using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Stores.Stores;

public class UiDefaultStoreProvider : IUiDefaultStoreProvider, IScopedDependency
{
    private StoreDto CachedDefaultStore { get; set; }

    protected IStoreAppService StoreAppService { get; }

    public UiDefaultStoreProvider(IStoreAppService storeAppService)
    {
        StoreAppService = storeAppService;
    }

    public virtual async Task<StoreDto> GetAsync()
    {
        return CachedDefaultStore ??= await StoreAppService.GetDefaultAsync();
    }
}