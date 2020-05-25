using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductManager : IDomainService
    {
        Task<Product> CreateAsync(Product product, Guid? storeId = null, IEnumerable<Guid> categoryIds = null);

        Task<Product> UpdateAsync(Product product, IEnumerable<Guid> categoryIds = null);

        Task DeleteAsync(Product product);
        
        Task DeleteAsync(Guid id);
        
        Task<bool> IsInventorySufficientAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
        
        Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId);

        Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
        
        Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity);
    }
}