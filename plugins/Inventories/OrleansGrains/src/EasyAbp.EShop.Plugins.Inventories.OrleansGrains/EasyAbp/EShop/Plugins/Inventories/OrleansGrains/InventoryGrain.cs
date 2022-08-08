using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Orleans;
using Orleans.Providers;
using Orleans.Runtime;

namespace EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

[StorageProvider(ProviderName = StorageProviderName)]
public class InventoryGrain : Grain<InventoryStateModel>, IInventoryGrain
{
    public const string StorageProviderName = "EShopInventoryStorage";

    protected bool FlashSalesInventoryUpdated { get; set; }

    [CanBeNull]
    protected IDisposable FlashSalesPersistInventoryTimer { get; set; }

    public virtual Task<InventoryStateModel> GetInventoryStateAsync() => Task.FromResult(State);

    public virtual async Task IncreaseInventoryAsync(int quantity, bool decreaseSold, bool isFlashSale)
    {
        InternalIncreaseInventory(quantity, decreaseSold);

        if (!isFlashSale || State.Inventory == 0)
        {
            await WriteStateAsync();
        }
        else
        {
            FlashSalesInventoryUpdated = true;

            TryRegisterFlashSalesPersistInventoryTimer();
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
            FlashSalesInventoryUpdated = true;

            TryRegisterFlashSalesPersistInventoryTimer();
        }
    }

    protected virtual void TryRegisterFlashSalesPersistInventoryTimer()
    {
        FlashSalesPersistInventoryTimer ??= RegisterTimer(PeriodicWriteInventoryStateAsync,
            new object(), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3));
    }

    protected virtual async Task PeriodicWriteInventoryStateAsync(object obj)
    {
        if (!FlashSalesInventoryUpdated)
        {
            return;
        }

        await WriteStateAsync();

        FlashSalesInventoryUpdated = false;
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