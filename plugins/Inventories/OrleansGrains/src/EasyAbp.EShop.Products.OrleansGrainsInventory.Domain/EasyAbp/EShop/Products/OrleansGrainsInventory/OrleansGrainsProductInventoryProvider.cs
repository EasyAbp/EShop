using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Inventories.OrleansGrains;
using EasyAbp.EShop.Products.ProductInventories;
using Microsoft.Extensions.Logging;
using Orleans;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.OrleansGrainsInventory;

public class OrleansGrainsProductInventoryProvider : IProductInventoryProvider, ITransientDependency
{
    public static string OrleansGrainsProductInventoryProviderName { get; set; } = "OrleansGrains";
    public static string OrleansGrainsProductInventoryProviderDisplayName { get; set; } = "OrleansGrains";
    public static string OrleansGrainsProductInventoryProviderDescription { get; set; } = "OrleansGrains";

    public string InventoryProviderName { get; } = OrleansGrainsProductInventoryProviderName;

    private readonly ILogger<OrleansGrainsProductInventoryProvider> _logger;
    protected IGrainFactory GrainFactory { get; }

    public OrleansGrainsProductInventoryProvider(
        IGrainFactory grainFactory,
        ILogger<OrleansGrainsProductInventoryProvider> logger)
    {
        GrainFactory = grainFactory;
        _logger = logger;
    }

    public virtual async Task<InventoryDataModel> GetInventoryDataAsync(InventoryQueryModel model)
    {
        var grain = await GetGrainAsync(model);

        var stateModel = await grain.GetInventoryStateAsync();

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
        var grain = await GetGrainAsync(model);

        try
        {
            await grain.IncreaseInventoryAsync(quantity, decreaseSold, isFlashSale);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Grain threw: {Message}", e.InnerException?.Message ?? e.Message);

            return false;
        }

        return true;
    }

    public virtual async Task<bool> TryReduceInventoryAsync(InventoryQueryModel model, int quantity, bool increaseSold,
        bool isFlashSale = false)
    {
        var grain = await GetGrainAsync(model);

        var stateModel = await grain.GetInventoryStateAsync();

        if (stateModel.Inventory < quantity)
        {
            return false;
        }

        try
        {
            await grain.ReduceInventoryAsync(quantity, increaseSold, isFlashSale);
        }
        catch (Exception e)
        {
            _logger.LogWarning("Grain threw: {Message}", e.InnerException?.Message ?? e.Message);

            return false;
        }

        return true;
    }

    protected virtual Task<IInventoryGrain> GetGrainAsync(InventoryQueryModel model)
    {
        return Task.FromResult(GrainFactory.GetGrain<IInventoryGrain>(GetGrainId(model)));
    }

    protected virtual string GetGrainId(InventoryQueryModel model)
    {
        return $"eshop_inventory_{(model.TenantId.HasValue ? model.TenantId.Value : "host")}_{model.ProductSkuId}";
    }
}