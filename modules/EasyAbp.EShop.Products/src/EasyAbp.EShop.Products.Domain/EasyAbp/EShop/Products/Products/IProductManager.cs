using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductManager : IDomainService
    {
        Task CheckPurchasableAsync(Product product, ProductSku productSku, Guid storeId,
            Dictionary<string, object> extraProperties);

        Task<CheckProductPurchasableResult> GetPurchasableStatusAsync(Product product, ProductSku productSku,
            Guid storeId, Dictionary<string, object> extraProperties);
        
        Task<bool> IsInventorySufficientAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
        
        Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId);

        Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
        
        Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
    }
}