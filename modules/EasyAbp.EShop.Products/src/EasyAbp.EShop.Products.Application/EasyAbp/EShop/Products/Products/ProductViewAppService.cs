using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Products.CacheItems;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Settings;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductViewAppService : MultiStoreReadOnlyAppService<ProductView, ProductViewDto, Guid, GetProductListInput>,
        IProductViewAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;

        private readonly IProductViewCacheKeyProvider _productViewCacheKeyProvider;
        private readonly IDistributedCache<ProductViewCacheItem> _cache;
        private readonly IProductManager _productManager;
        private readonly IProductRepository _productRepository;
        private readonly IProductViewRepository _repository;
        
        public ProductViewAppService(
            IProductViewCacheKeyProvider productViewCacheKeyProvider,
            IDistributedCache<ProductViewCacheItem> cache,
            IProductManager productManager,
            IProductRepository productRepository,
            IProductViewRepository repository) : base(repository)
        {
            _productViewCacheKeyProvider = productViewCacheKeyProvider;
            _cache = cache;
            _productManager = productManager;
            _productRepository = productRepository;
            _repository = repository;
        }
        
        protected override async Task<IQueryable<ProductView>> CreateFilteredQueryAsync(GetProductListInput input)
        {
            var query = input.CategoryId.HasValue
                ? await _repository.WithDetailsAsync(input.CategoryId.Value)
                : await _repository.WithDetailsAsync();

            return query
                .Where(x => x.StoreId == input.StoreId)
                .WhereIf(!input.ShowHidden, x => !x.IsHidden)
                .WhereIf(!input.ShowUnpublished, x => x.IsPublished);
        }

        public override async Task<PagedResultDto<ProductViewDto>> GetListAsync(GetProductListInput input)
        {
            await CheckGetListPolicyAsync();
            
            if (input.ShowHidden || input.ShowUnpublished)
            {
                await CheckMultiStorePolicyAsync(input.StoreId, ProductsPermissions.Products.Manage);
            }

            if (await _cache.GetAsync(await GetCacheKeyAsync(input.StoreId), true) == null)
            {
                await BuildStoreProductViewsAsync(input.StoreId);
            }
            
            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var productViews = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(productViews);

            return new PagedResultDto<ProductViewDto>(
                totalCount,
                entityDtos
            );
        }

        protected virtual async Task<string> GetCacheKeyAsync(Guid storeId)
        {
            return await _productViewCacheKeyProvider.GetCacheKeyAsync(storeId);
        }

        public override async Task<ProductViewDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var productView = await GetEntityByIdAsync(id);

            if (await _cache.GetAsync(await GetCacheKeyAsync(productView.StoreId), true) != null)
            {
                return await base.GetAsync(id);
            }

            await BuildStoreProductViewsAsync(productView.StoreId);

            productView = await GetEntityByIdAsync(id);
            
            return await MapToGetOutputDtoAsync(productView);
        }

        protected virtual async Task BuildStoreProductViewsAsync(Guid storeId)
        {
            var products = await _productRepository.GetListAsync(x => x.StoreId == storeId, true);

            using var uow = UnitOfWorkManager.Begin(true, true);

            await _repository.DeleteAsync(x => x.StoreId == storeId, true);

            foreach (var product in products)
            {
                var productView = ObjectMapper.Map<Product, ProductView>(product);

                await FillPriceInfoWithRealPriceAsync(product, productView);
                
                await _repository.InsertAsync(productView);
            }

            await uow.SaveChangesAsync();
            await uow.CompleteAsync();

            await _cache.SetAsync(await GetCacheKeyAsync(storeId), new ProductViewCacheItem(),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(Convert.ToInt32(
                        await SettingProvider.GetOrNullAsync(ProductsSettings.ProductView.CacheDurationSeconds)))
                });
        }
        
        protected virtual async Task FillPriceInfoWithRealPriceAsync(Product product, ProductView productView)
        {
            if (product.ProductSkus.IsNullOrEmpty())
            {
                return;
            }

            decimal? min = null, max = null;
            
            foreach (var productSku in product.ProductSkus)
            {
                var priceDataModel = await _productManager.GetRealPriceAsync(product, productSku);

                if (min is null || priceDataModel.Price < min.Value)
                {
                    min = productSku.Price;
                }

                if (max is null || priceDataModel.Price > max.Value)
                {
                    max = productSku.Price;
                }
            }

            productView.SetPrices(min, max);
        }
    }
}
