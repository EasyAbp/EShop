using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductManager : DomainService, IProductManager
    {
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
    }
}