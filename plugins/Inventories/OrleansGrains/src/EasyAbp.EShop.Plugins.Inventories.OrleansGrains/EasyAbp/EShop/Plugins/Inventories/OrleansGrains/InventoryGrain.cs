using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

[StorageProvider(ProviderName = StorageProviderName)]
public class InventoryGrain : Grain<InventoryStateModel>, IInventoryGrain
{
    public const string StorageProviderName = "EShopInventoryStorage";

    public virtual Task<InventoryStateModel> GetInventoryStateAsync() => Task.FromResult(State);

    public virtual async Task IncreaseInventoryAsync(int quantity, bool decreaseSold)
    {
        InternalIncreaseInventory(quantity, decreaseSold);

        await WriteStateAsync();
    }

    public async Task ReduceInventoryAsync(int quantity, bool increaseSold)
    {
        InternalReduceInventory(quantity, increaseSold);

        await WriteStateAsync();
    }

    protected virtual void InternalIncreaseInventory(int quantity, bool decreaseSold)
    {
        if (quantity < 0)
        {
            throw new OrleansException("Quantity should not be less than 0.");
        }

        if (decreaseSold && State.Sold - quantity < 0)
        {
            throw new OrleansException("Target Sold cannot be less than 0.");
        }

        State.Inventory = checked(State.Inventory + quantity);

        if (decreaseSold)
        {
            State.Sold -= quantity;
        }
    }

    protected virtual void InternalReduceInventory(int quantity, bool increaseSold)
    {
        if (quantity < 0)
        {
            throw new OrleansException("Quantity should not be less than 0.");
        }

        if (quantity > State.Inventory)
        {
            throw new OrleansException("Insufficient inventory.");
        }

        if (increaseSold)
        {
            State.Sold = checked(State.Sold + quantity);
        }

        State.Inventory -= quantity;
    }
}