using System.Threading.Tasks;
using Dapr;
using Dapr.Actors.Runtime;

namespace EasyAbp.EShop.Plugins.Inventories.DaprActors;

public class InventoryActor : Actor, IInventoryActor
{
    public static string InventoryStateName { get; set; } = "i";

    public InventoryActor(ActorHost host) : base(host)
    {
    }

    protected override async Task OnActivateAsync()
    {
        await StateManager.TryAddStateAsync(InventoryStateName, new InventoryStateModel());
    }

    public virtual async Task<InventoryStateModel> GetInventoryStateAsync()
    {
        return await StateManager.GetStateAsync<InventoryStateModel>(InventoryStateName);
    }

    public virtual async Task IncreaseInventoryAsync(int quantity, bool decreaseSold)
    {
        var state = await GetInventoryStateAsync();

        InternalIncreaseInventory(state, quantity, decreaseSold);

        await SetInventoryStateAsync(state);
    }

    public async Task ReduceInventoryAsync(int quantity, bool increaseSold)
    {
        var state = await GetInventoryStateAsync();

        InternalReduceInventory(state, quantity, increaseSold);

        await SetInventoryStateAsync(state);
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