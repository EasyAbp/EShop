using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public interface IFlashSaleCurrentResultCache
{
    Task<FlashSaleCurrentResultCacheItem> GetAsync(Guid planId, Guid userId);

    Task SetAsync(Guid planId, Guid userId, FlashSaleCurrentResultCacheItem cacheItem);

    Task RemoveAsync(Guid planId, Guid userId);
}