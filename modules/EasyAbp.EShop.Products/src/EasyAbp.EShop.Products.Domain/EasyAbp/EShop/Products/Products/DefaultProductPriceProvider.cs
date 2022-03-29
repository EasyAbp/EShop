using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    public class DefaultProductPriceProvider : IProductPriceProvider, ITransientDependency
    {
        public virtual Task<decimal> GetPriceAsync(IProduct product, IProductSku productSku)
        {
            return Task.FromResult(productSku.Price);
        }
    }
}