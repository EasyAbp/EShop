using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;
using EasyAbp.EShop.Plugins.Inventories.DaprActors;
using EasyAbp.EShop.Products.ProductInventories;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.DaprActorsInventory;

public class DaprActorsProductInventoryProvider : IProductInventoryProvider, ITransientDependency
{
    public static string ActorType { get; set; } = "InventoryActor";
    public static string DaprActorsProductInventoryProviderName { get; set; } = "DaprActors";
    public static string DaprActorsProductInventoryProviderDisplayName { get; set; } = "DaprActors";
    public static string DaprActorsProductInventoryProviderDescription { get; set; } = "DaprActors";

    public string InventoryProviderName { get; } = DaprActorsProductInventoryProviderName;

    private readonly ILogger<DaprActorsProductInventoryProvider> _logger;
    protected IActorProxyFactory ActorProxyFactory { get; }

    public DaprActorsProductInventoryProvider(
        IActorProxyFactory actorProxyFactory,
        ILogger<DaprActorsProductInventoryProvider> logger)
    {
        ActorProxyFactory = actorProxyFactory;
        _logger = logger;
    }

    public virtual async Task<InventoryDataModel> GetInventoryDataAsync(InventoryQueryModel model)
    {
        var actor = await GetActorAsync(model);

        var stateModel = await actor.GetInventoryStateAsync();

        return new InventoryDataModel
        {
            Inventory = stateModel.Inventory,
            Sold = stateModel.Sold
        };
    }

    public virtual async Task<Dictionary<Guid, InventoryDataModel>> GetSkuIdInventoryDataMappingAsync(
        IList<InventoryQueryModel> models)
    {
        var result = new Dictionary<Guid, InventoryDataModel>();

        foreach (var model in models)
        {
            result.Add(model.ProductSkuId, await GetInventoryDataAsync(model));
        }

        return result;
    }

    public virtual async Task<bool> TryIncreaseInventoryAsync(InventoryQueryModel model, int quantity,
        bool decreaseSold, bool isFlashSale = false)
    {
        var actor = await GetActorAsync(model);

        try
        {
            await actor.IncreaseInventoryAsync(quantity, decreaseSold, isFlashSale);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Actor threw: {Message}", e.InnerException?.Message ?? e.Message);

            return false;
        }

        return true;
    }

    public virtual async Task<bool> TryReduceInventoryAsync(InventoryQueryModel model, int quantity, bool increaseSold,
        bool isFlashSale = false)
    {
        var actor = await GetActorAsync(model);

        var stateModel = await actor.GetInventoryStateAsync();

        if (stateModel.Inventory < quantity)
        {
            return false;
        }

        try
        {
            await actor.ReduceInventoryAsync(quantity, increaseSold, isFlashSale);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Actor threw: {Message}", e.InnerException?.Message ?? e.Message);

            return false;
        }

        return true;
    }

    protected virtual Task<IInventoryActor> GetActorAsync(InventoryQueryModel model)
    {
        return Task.FromResult(ActorProxyFactory.CreateActorProxy<IInventoryActor>(GetActorId(model), ActorType));
    }

    protected virtual ActorId GetActorId(InventoryQueryModel model)
    {
        return new ActorId(
            $"eshop_inventory_{(model.TenantId.HasValue ? model.TenantId.Value : "host")}_{model.ProductSkuId}");
    }
}