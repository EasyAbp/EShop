using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products.CacheItems
{
    public class ProductViewCacheKeyProvider : IProductViewCacheKeyProvider, ITransientDependency
    {
        public virtual Task<string> GetCacheKeyAsync(Guid storeId)
        {
            return Task.FromResult(storeId.ToString());
        }
    }
}