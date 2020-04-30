using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductPurchasableStatusProvider : IProductPurchasableStatusProvider, ITransientDependency
    {
        public async Task CheckPurchasableAsync(Product product, ProductSku productSku, Guid storeId)
        {
            var result = await GetPurchasableStatusAsync(product, productSku, storeId);
            
            if  (result.PurchasableStatus != ProductPurchasableStatus.CanBePurchased)
            {
                throw new ProductIsNotPurchasableException(product.Id, result.Reason);
            }
        }

        public async Task<GetProductPurchasableStatusResult> GetPurchasableStatusAsync(Product product, ProductSku productSku, Guid storeId)
        {
            // Todo: Determine whether the product can be purchased.
            return new GetProductPurchasableStatusResult
            {
                PurchasableStatus = ProductPurchasableStatus.CanBePurchased,
                Reason = null
            };
        }
    }
}