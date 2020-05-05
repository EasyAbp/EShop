using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductManager : DomainService, IProductManager
    {
        private readonly IProductInventoryProvider _productInventoryProvider;

        public ProductManager(
            IProductInventoryProvider productInventoryProvider)
        {
            _productInventoryProvider = productInventoryProvider;
        }
        
        public async Task CheckPurchasableAsync(Product product, ProductSku productSku, Guid storeId,
            Dictionary<string, object> extraProperties)
        {
            var result = await GetPurchasableStatusAsync(product, productSku, storeId, extraProperties);

            if (!result.IsPurchasable)
            {
                throw new ProductIsNotPurchasableException(product.Id, result.Reason);
            }
        }

        public async Task<CheckProductPurchasableResult> GetPurchasableStatusAsync(Product product,
            ProductSku productSku, Guid storeId, Dictionary<string, object> extraProperties)
        {
            var handlers = ServiceProvider.GetServices<IProductPurchasableCheckHandler>();

            foreach (var handler in handlers)
            {
                var result = await handler.CheckAsync(product, productSku, storeId, extraProperties);

                if (!result.IsPurchasable)
                {
                    return result;
                }
            }

            return new CheckProductPurchasableResult(true);
        }

        public async Task<bool> IsInventorySufficientAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return await _productInventoryProvider.IsInventorySufficientAsync(product, productSku, storeId, quantity);
        }

        public async Task<int> GetInventoryAsync(Product product, ProductSku productSku, Guid storeId)
        {
            return await _productInventoryProvider.GetInventoryAsync(product, productSku, storeId);
        }

        public async Task<bool> TryIncreaseInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return await _productInventoryProvider.TryIncreaseInventoryAsync(product, productSku, storeId, quantity);
        }

        public async Task<bool> TryReduceInventoryAsync(Product product, ProductSku productSku, Guid storeId, int quantity)
        {
            return await _productInventoryProvider.TryReduceInventoryAsync(product, productSku, storeId, quantity);
        }
    }
}