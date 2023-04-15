using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores.Dtos;

namespace EasyAbp.EShop.Stores.Stores;

public interface IUiDefaultStoreProvider
{
    Task<StoreDto> GetAsync();
}