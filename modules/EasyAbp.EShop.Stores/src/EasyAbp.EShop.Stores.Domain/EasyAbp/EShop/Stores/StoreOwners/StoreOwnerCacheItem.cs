using System;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    [Serializable]
    public class StoreOwnerCacheItem
    {
        public bool IsOwner { get; set; }

        public StoreOwnerCacheItem()
        {
        }

        public StoreOwnerCacheItem(bool isOwner)
        {
            IsOwner = isOwner;
        }

        public static string CalculateCacheKey(Guid storeId, Guid userId)
        {
            return "s:" + storeId.ToString("D") + ",u:" + userId.ToString("D");
        }
    }
}