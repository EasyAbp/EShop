using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductInventoryProvider
    {
        Task<InventoryDataModel> GetInventoryDataAsync(Product product, ProductSku productSku, Guid storeId);
        
        Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(Product product, Guid storeId);

        Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity, bool decreaseSold);
        
        Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity, bool increaseSold);
    }
}