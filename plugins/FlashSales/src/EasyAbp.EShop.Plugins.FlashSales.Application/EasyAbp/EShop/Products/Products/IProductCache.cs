using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products;

public interface IProductCache
{
    Task<ProductCacheItem> GetAsync(Guid productId);

    Task RemoveAsync(Guid productId);
}