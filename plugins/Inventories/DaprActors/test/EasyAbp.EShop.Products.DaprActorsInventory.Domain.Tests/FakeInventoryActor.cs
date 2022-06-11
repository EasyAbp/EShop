using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Inventories.DaprActors;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.DaprActorsInventory.Domain;

public class FakeInventoryActor : IInventoryActor, ITransientDependency
{
    private InventoryStateModel StateModel { get; } = new()
    {
        Inventory = 100,
        Sold = 0
    };

    public Task<InventoryStateModel> GetInventoryStateAsync()
    {
        return Task.FromResult(StateModel);
    }

    public Task IncreaseInventoryAsync(int quantity, bool decreaseSold)
    {
        StateModel.Inventory += quantity;

        if (decreaseSold)
        {
            StateModel.Sold -= quantity;
        }

        return Task.CompletedTask;
    }

    public Task ReduceInventoryAsync(int quantity, bool increaseSold)
    {
        StateModel.Inventory -= quantity;

        if (increaseSold)
        {
            StateModel.Sold += quantity;
        }

        return Task.CompletedTask;
    }
}