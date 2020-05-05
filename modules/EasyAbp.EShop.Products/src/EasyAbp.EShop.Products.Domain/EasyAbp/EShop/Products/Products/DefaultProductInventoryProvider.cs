using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    [Dependency(TryRegister = true)]
    public class DefaultProductInventoryProvider : IProductInventoryProvider, ITransientDependency
    {
        public virtual async Task<bool> IsInventorySufficientAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            var inventory = await GetInventoryAsync(product, productSku, storeId);
            
            return product.InventoryStrategy == InventoryStrategy.NoNeed || inventory - quantity >= 0;
        }

        public virtual Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId)
        {
            return Task.FromResult(productSku.Inventory);
        }

        public Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return Task.FromResult(productSku.TryIncreaseInventory(quantity));
        }

        public Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return Task.FromResult(productSku.TryReduceInventory(quantity));
        }
    }
}