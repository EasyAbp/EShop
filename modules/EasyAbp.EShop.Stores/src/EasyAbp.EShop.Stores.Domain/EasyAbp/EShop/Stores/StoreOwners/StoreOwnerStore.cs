using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerStore : IStoreOwnerStore, ITransientDependency
    {
        protected ILogger<StoreOwnerStore> Logger { get; }
        protected IStoreOwnerRepository StoreOwnerRepository { get; }
        protected IDistributedCache<StoreOwnerCacheItem> Cache { get; }

        public StoreOwnerStore(ILogger<StoreOwnerStore> logger,
            IStoreOwnerRepository storeOwnerRepository,
            IDistributedCache<StoreOwnerCacheItem> cache)
        {
            StoreOwnerRepository = storeOwnerRepository;
            Cache = cache;
            Logger = logger ?? NullLogger<StoreOwnerStore>.Instance;
        }

        public async Task<bool> IsStoreOwnerAsync(Guid storeId, Guid userId)
        {
            return (await GetCacheItemAsync(storeId, userId)).IsOwner;
        }

        protected virtual async Task<StoreOwnerCacheItem> GetCacheItemAsync(Guid storeId, Guid userId)
        {
            var cacheKey = CalculateCacheKey(storeId, userId);
            Logger.LogDebug($"StoreOwnerStore.GetCacheItemAsync: {cacheKey}");

            var cacheItem = await Cache.GetAsync(cacheKey);
            if (cacheItem != null)
            {
                Logger.LogDebug($"Found in the cache: {cacheKey}");
                return cacheItem;
            }

            Logger.LogDebug($"Not found in the cache: {cacheKey}");
            cacheItem = new StoreOwnerCacheItem(false);

            await SetCacheItemsAsync(storeId, userId, cacheItem);
            return cacheItem;
        }

        protected virtual async Task SetCacheItemsAsync(
            Guid storeId, Guid userId,
            StoreOwnerCacheItem currentCacheItem)
        {
            var storeOwners = await StoreOwnerRepository.GetListByStoreIdAsync(storeId);
            Logger.LogDebug(
                $"Getting all store owner in store: {storeId}");

            Logger.LogDebug($"Setting the cache items. Count: {storeOwners.Count}");

            var cacheItems = new Dictionary<string, StoreOwnerCacheItem>();
            foreach (var storeOwner in storeOwners)
            {
                cacheItems.Add(CalculateCacheKey(storeOwner.StoreId, storeOwner.OwnerId),
                    new StoreOwnerCacheItem(true));

                if (storeOwner.StoreId == storeId && storeOwner.OwnerId == userId)
                {
                    currentCacheItem.IsOwner = true;
                }
            }

            await Cache.SetManyAsync(cacheItems);
            Logger.LogDebug($"Finished setting the cache items. Count: {storeOwners.Count}");
        }

        protected virtual string CalculateCacheKey(Guid storeId, Guid userId)
        {
            return StoreOwnerCacheItem.CalculateCacheKey(storeId, userId);
        }
    }
}