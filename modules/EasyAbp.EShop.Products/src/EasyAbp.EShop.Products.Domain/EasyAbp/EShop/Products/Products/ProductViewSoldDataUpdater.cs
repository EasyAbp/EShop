using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.Products.CacheItems;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductViewSoldDataUpdater : IDistributedEventHandler<ProductInventoryChangedEto>,
        IProductViewSoldDataUpdater, ITransientDependency
    {
        private readonly IProductViewCacheKeyProvider _productViewCacheKeyProvider;
        private readonly IDistributedCache<ProductViewCacheItem> _cache;

        public ProductViewSoldDataUpdater(
            IProductViewCacheKeyProvider productViewCacheKeyProvider,
            IDistributedCache<ProductViewCacheItem> cache)
        {
            _productViewCacheKeyProvider = productViewCacheKeyProvider;
            _cache = cache;
        }
        
        public async Task HandleEventAsync(ProductInventoryChangedEto eventData)
        {
            await ClearProductViewCacheAsync(eventData.StoreId);
        }
        
        protected virtual async Task ClearProductViewCacheAsync(Guid storeId)
        {
            await _cache.RemoveAsync(await _productViewCacheKeyProvider.GetCacheKeyAsync(storeId));
        }
    }
}