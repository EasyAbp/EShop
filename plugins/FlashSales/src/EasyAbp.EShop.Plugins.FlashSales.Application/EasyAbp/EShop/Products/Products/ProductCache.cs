using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.EShop.Products.Products;

public class ProductCache : IProductCache, ITransientDependency
{
    protected IDistributedCache<ProductCacheItem, Guid> DistributedCache { get; }

    protected IProductAppService ProductAppService { get; }

    protected IObjectMapper ObjectMapper { get; }

    protected ICurrentTenant CurrentTenant { get; }

    public ProductCache(
        IDistributedCache<ProductCacheItem, Guid> productDistributedCache,
        IProductAppService productAppService,
        IObjectMapper objectMapper,
        ICurrentTenant currentTenant)
    {
        DistributedCache = productDistributedCache;
        ProductAppService = productAppService;
        ObjectMapper = objectMapper;
        CurrentTenant = currentTenant;
    }

    public virtual async Task<ProductCacheItem> GetAsync(Guid productId)
    {
        return await DistributedCache.GetOrAddAsync(productId, async () =>
        {
            var productDto = await ProductAppService.GetAsync(productId);

            var cacheItem = ObjectMapper.Map<ProductDto, ProductCacheItem>(productDto);

            if (cacheItem != null)
            {
                cacheItem.TenantId = CurrentTenant.Id;
            }

            return cacheItem;
        });
    }

    public virtual async Task RemoveAsync(Guid productId)
    {
        await DistributedCache.RemoveAsync(productId);
    }
}
