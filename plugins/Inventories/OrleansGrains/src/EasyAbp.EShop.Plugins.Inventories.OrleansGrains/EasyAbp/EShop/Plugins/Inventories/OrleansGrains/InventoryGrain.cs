using System;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

[StorageProvider(ProviderName = StorageProviderName)]
public class InventoryGrain : Grain<InventoryStateModel>, IInventoryGrain
{
    public const string StorageProviderName = "EShopInventoryStorage";

    protected DateTime? TimeToPersistInventory { get; set; }

    protected IClock Clock { get; }

    public InventoryGrain(IClock clock)
    {
        Clock = clock;
    }

    public virtual async Task<InventoryStateModel> GetInventoryStateAsync()
    {
        var state = State;

        await TryWriteStateAsync();

        return state;
    }

    public virtual async Task IncreaseInventoryAsync(int quantity, bool decreaseSold, bool isFlashSale)
    {
        InternalIncreaseInventory(quantity, decreaseSold);

        if (!isFlashSale || State.Inventory == 0)
        {
            await WriteStateAsync();
        }
        else
        {
            TimeToPersistInventory ??= Clock.Now + TimeSpan.FromSeconds(30);

            await TryWriteStateAsync();
        }
    }

    public virtual async Task ReduceInventoryAsync(int quantity, bool increaseSold, bool isFlashSale)
    {
        InternalReduceInventory(quantity, increaseSold);

        if (!isFlashSale || State.Inventory == 0)
        {
            await WriteStateAsync();
        }
        else
        {
            TimeToPersistInventory ??= Clock.Now + TimeSpan.FromSeconds(30);

            await TryWriteStateAsync();
        }
    }
    
    public async Task<bool> TryWriteStateAsync()
    {
        if (!TimeToPersistInventory.HasValue || TimeToPersistInventory.Value < Clock.Now)
        {
            return false;
        }

        await WriteStateAsync();

        return true;
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