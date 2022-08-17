using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class FlashSaleCurrentResultCache : IFlashSaleCurrentResultCache, ITransientDependency
{
    /// <summary>
    /// The <see cref="GetKeyAsync(Guid,Guid)"/> cache key format.
    /// <para>{0}: FlashSalePlan ID</para>
    /// <para>{1}: User ID</para>
    /// </summary>
    public const string FlashSaleCurrentResultCacheKeyFormat = "eshopflashsales-current-result_{0}_{1}";

    protected IDistributedCache<FlashSaleCurrentResultCacheItem> Cache { get; }
    protected FlashSalesOptions Options { get; }

    public FlashSaleCurrentResultCache(
        IDistributedCache<FlashSaleCurrentResultCacheItem> cache,
        IOptions<FlashSalesOptions> options)
    {
        Cache = cache;
        Options = options.Value;
    }

    public virtual async Task<FlashSaleCurrentResultCacheItem> GetAsync(Guid planId, Guid userId)
    {
        var flashSaleCurrentResultCacheKey = await GetKeyAsync(planId, userId);
        return await Cache.GetAsync(flashSaleCurrentResultCacheKey);
    }

    public virtual async Task SetAsync(Guid planId, Guid userId, FlashSaleCurrentResultCacheItem cacheItem)
    {
        var flashSaleCurrentResultCacheKey = await GetKeyAsync(planId, userId);

        await Cache.SetAsync(flashSaleCurrentResultCacheKey, cacheItem,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.Add(Options.FlashSaleCurrentResultCacheExpires)
            });
    }

    public virtual async Task RemoveAsync(Guid planId, Guid userId)
    {
        var flashSaleCurrentResultCacheKey = await GetKeyAsync(planId, userId);

        await Cache.RemoveAsync(flashSaleCurrentResultCacheKey);
    }

    protected virtual Task<string> GetKeyAsync(Guid planId, Guid userId)
    {
        return Task.FromResult(string.Format(FlashSaleCurrentResultCacheKeyFormat, planId, userId));
    }
}