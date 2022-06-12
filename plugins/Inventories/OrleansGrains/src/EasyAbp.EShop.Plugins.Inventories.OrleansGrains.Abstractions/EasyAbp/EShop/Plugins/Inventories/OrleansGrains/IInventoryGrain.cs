using System.Threading.Tasks;
using Orleans;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

public interface IInventoryGrain : IGrainWithStringKey
{
    Task<InventoryStateModel> GetInventoryStateAsync();

    Task IncreaseInventoryAsync(int quantity, bool decreaseSold);

    Task ReduceInventoryAsync(int quantity, bool increaseSold);
}