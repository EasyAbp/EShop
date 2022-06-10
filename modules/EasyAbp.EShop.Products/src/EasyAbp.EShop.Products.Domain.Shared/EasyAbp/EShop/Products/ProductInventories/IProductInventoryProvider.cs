using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public interface IProductInventoryProvider
    {
        Task<InventoryDataModel> GetInventoryDataAsync(InventoryQueryModel model);

        Task<Dictionary<Guid, InventoryDataModel>> GetSkuIdInventoryDataMappingAsync(IList<InventoryQueryModel> models);

        Task<bool> TryIncreaseInventoryAsync(InventoryQueryModel model, int quantity, bool decreaseSold);
        
        Task<bool> TryReduceInventoryAsync(InventoryQueryModel model, int quantity, bool increaseSold);
    }
}