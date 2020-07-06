using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductInventoryProvider
    {
        string ProviderName { get; }
        
        Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId);
        
        Task<Dictionary<Guid, int>> GetInventoryDictionaryAsync(Product product, Guid storeId);

        Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
        
        Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
    }
}