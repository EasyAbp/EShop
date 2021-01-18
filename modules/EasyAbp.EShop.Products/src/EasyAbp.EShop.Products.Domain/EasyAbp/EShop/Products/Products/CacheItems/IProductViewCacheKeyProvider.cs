using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products.CacheItems
{
    public interface IProductViewCacheKeyProvider
    {
        Task<string> GetCacheKeyAsync(Guid storeId);
    }
}