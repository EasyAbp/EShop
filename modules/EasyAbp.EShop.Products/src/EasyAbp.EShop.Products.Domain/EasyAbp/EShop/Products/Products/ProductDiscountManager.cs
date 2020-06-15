using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductDiscountManager : DomainService, IProductDiscountManager
    {
        public async Task<decimal> GetDiscountedPriceAsync(Product product, ProductSku productSku, Guid storeId)
        {
            var currentPrice = productSku.Price;
            
            foreach (var provider in ServiceProvider.GetServices<IProductDiscountProvider>())
            {
                currentPrice = await provider.GetDiscountedPriceAsync(product, productSku, storeId, currentPrice);
            }

            return currentPrice;
        }
    }
}