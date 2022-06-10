using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products;

public class FakeProductInventoryProvider : IProductInventoryProvider, ITransientDependency
{
    public string InventoryProviderName { get; } = "Fake";

    private static InventoryDataModel Model { get; } = new()
    {
        Inventory = 9998,
        Sold = 0
    };

    public Task<InventoryDataModel> GetInventoryDataAsync(InventoryQueryModel model)
    {
        return Task.FromResult(Model);
    }

    public Task<Dictionary<Guid, InventoryDataModel>> GetSkuIdInventoryDataMappingAsync(
        IList<InventoryQueryModel> models)
    {
        var result = new Dictionary<Guid, InventoryDataModel>();

        foreach (var model in models)
        {
            result.Add(model.ProductSkuId, Model);
        }

        return Task.FromResult(result);
    }

    public Task<bool> TryIncreaseInventoryAsync(InventoryQueryModel model, int quantity, bool decreaseSold)
    {
        Model.Inventory++;
        
        if (decreaseSold)
        {
            Model.Sold--;
        }

        return Task.FromResult(true);
    }

    public Task<bool> TryReduceInventoryAsync(InventoryQueryModel model, int quantity, bool increaseSold)
    {
        Model.Inventory--;
        
        if (increaseSold)
        {
            Model.Sold++;
        }

        return Task.FromResult(true);
    }
}