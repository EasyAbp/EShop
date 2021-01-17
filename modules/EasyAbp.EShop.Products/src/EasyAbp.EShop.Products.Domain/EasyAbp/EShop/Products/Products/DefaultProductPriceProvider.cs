using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    public class DefaultProductPriceProvider : IProductPriceProvider, ITransientDependency
    {
        public virtual Task<decimal> GetPriceAsync(Product product, ProductSku productSku)
        {
            return Task.FromResult(productSku.Price);
        }
    }
}