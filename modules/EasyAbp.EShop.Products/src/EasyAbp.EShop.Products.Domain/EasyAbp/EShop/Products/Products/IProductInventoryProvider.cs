using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductInventoryProvider
    {
        Task<InventoryDataModel> GetInventoryDataAsync(Product product, ProductSku productSku);
        
        Task<Dictionary<Guid, InventoryDataModel>> GetInventoryDataDictionaryAsync(Product product);

        Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, int quantity, bool decreaseSold);
        
        Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, int quantity, bool increaseSold);
    }
}