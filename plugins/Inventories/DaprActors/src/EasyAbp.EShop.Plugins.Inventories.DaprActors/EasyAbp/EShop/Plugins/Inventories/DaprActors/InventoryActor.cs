using System;
using System.Threading.Tasks;
using Dapr;
using Dapr.Actors.Runtime;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

public class InventoryActor : Actor, IInventoryActor
{
    public static string InventoryStateName { get; set; } = "i";

    protected DateTime? TimeToPersistInventory { get; set; }

    protected IClock Clock { get; }

    public InventoryActor(ActorHost host, IClock clock) : base(host)
    {
        Clock = clock;
    }

    protected override async Task OnActivateAsync()
    {
        await StateManager.TryAddStateAsync(InventoryStateName, new InventoryStateModel());
    }

    public virtual async Task<InventoryStateModel> GetInventoryStateAsync()
    {
        var state = await InternalGetInventoryStateAsync();

        await TrySetInventoryStateAsync(state);

        return state;
    }

    public virtual async Task IncreaseInventoryAsync(int quantity, bool decreaseSold, bool isFlashSale)
    {
        var state = await InternalGetInventoryStateAsync();

        InternalIncreaseInventory(state, quantity, decreaseSold);

        if (!isFlashSale || state.Inventory == 0)
        {
            await SetInventoryStateAsync(state);
        }
        else
        {
            TimeToPersistInventory ??= Clock.Now + TimeSpan.FromSeconds(30);

            await TrySetInventoryStateAsync(state);
        }
    }

    public virtual async Task ReduceInventoryAsync(int quantity, bool increaseSold, bool isFlashSale)
    {
        var state = await InternalGetInventoryStateAsync();

        InternalReduceInventory(state, quantity, increaseSold);

        if (!isFlashSale || state.Inventory == 0)
        {
            await SetInventoryStateAsync(state);
        }
        else
        {
            TimeToPersistInventory ??= Clock.Now + TimeSpan.FromSeconds(30);

            await TrySetInventoryStateAsync(state);
        }
    }

    protected virtual Task<InventoryStateModel> InternalGetInventoryStateAsync() =>
        StateManager.GetStateAsync<InventoryStateModel>(InventoryStateName);

    protected virtual async Task<bool> TrySetInventoryStateAsync(InventoryStateModel stateModel)
    {
        if (!TimeToPersistInventory.HasValue || TimeToPersistInventory.Value < Clock.Now)
        {
            return false;
        }

        await SetInventoryStateAsync(stateModel);

        return true;
    }

    protected virtual async Task SetInventoryStateAsync(InventoryStateModel state)
    {
        await StateManager.SetStateAsync(InventoryStateName, state);
    }

    protected virtual void InternalIncreaseInventory(InventoryStateModel stateModel, int quantity, bool decreaseSold)
    {
        if (quantity < 0)
        {
            throw new DaprException("Quantity should not be less than 0.");
        }

        if (decreaseSold && stateModel.Sold - quantity < 0)
        {
            throw new DaprException("Target Sold cannot be less than 0.");
        }

        stateModel.Inventory = checked(stateModel.Inventory + quantity);

        if (decreaseSold)
        {
            stateModel.Sold -= quantity;
        }
    }

    protected virtual void InternalReduceInventory(InventoryStateModel stateModel, int quantity, bool increaseSold)
    {
        if (quantity < 0)
        {
            throw new DaprException("Quantity should not be less than 0.");
        }

        if (quantity > stateModel.Inventory)
        {
            throw new DaprException("Insufficient inventory.");
        }

        if (increaseSold)
        {
            stateModel.Sold = checked(stateModel.Sold + quantity);
        }

        stateModel.Inventory -= quantity;
    }
}