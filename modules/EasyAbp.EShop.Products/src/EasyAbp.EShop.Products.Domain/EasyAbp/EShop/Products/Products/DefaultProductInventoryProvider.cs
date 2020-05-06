using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    [Dependency(TryRegister = true)]
    public class DefaultProductInventoryProvider : IProductInventoryProvider, ITransientDependency
    {
        public virtual Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId)
        {
            return Task.FromResult(productSku.Inventory);
        }

        public virtual Task<Dictionary<Guid, int>> GetInventoryDictionaryAsync(Product product, Guid storeId)
        {
            var dict = new Dictionary<Guid, int>();
            
            foreach (var productSku in product.ProductSkus)
            {
                dict[productSku.Id] = productSku.Inventory;
            }

            return Task.FromResult(dict);
        }

        public virtual Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return Task.FromResult(productSku.TryIncreaseInventory(quantity));
        }

        public virtual Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return Task.FromResult(productSku.TryReduceInventory(quantity));
        }
    }
}