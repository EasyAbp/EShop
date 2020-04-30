using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    public class BasicProductPurchasableCheckHandler : IProductPurchasableCheckHandler, ITransientDependency
    {
        private readonly IProductInventoryProvider _productInventoryProvider;

        public BasicProductPurchasableCheckHandler(
            IProductInventoryProvider productInventoryProvider)
        {
            _productInventoryProvider = productInventoryProvider;
        }
        
        public async Task<CheckProductPurchasableResult> CheckAsync(Product product, ProductSku productSku, Guid storeId,
            Dictionary<string, object> extraProperties)
        {
            if (!await IsProductPublishedAsync(product))
            {
                return new CheckProductPurchasableResult(false, "Unpublished project");
            }
            
            if (!await IsInventorySufficientAsync(product, productSku, storeId, extraProperties))
            {
                return new CheckProductPurchasableResult(false, "Insufficient inventory");
            }

            return new CheckProductPurchasableResult(true);
        }

        protected virtual Task<bool> IsProductPublishedAsync(Product product)
        {
            return Task.FromResult(product.IsPublished);
        }

        protected virtual async Task<bool> IsInventorySufficientAsync(Product product, ProductSku productSku, Guid storeId, Dictionary<string, object> extraProperties)
        {
            if (!extraProperties.TryGetValue("Quantity", out var quantity))
            {
                throw new ProductPurchasableCheckHandlerMissingPropertyException(
                    nameof(BasicProductPurchasableCheckHandler), "Quantity");
            }

            return await _productInventoryProvider.IsInventorySufficientAsync(product, productSku, storeId,
                Convert.ToInt32(quantity));
        }
    }
}