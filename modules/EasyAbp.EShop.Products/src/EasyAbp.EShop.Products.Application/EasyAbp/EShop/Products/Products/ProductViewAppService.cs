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
using Volo.Abp.Caching;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductViewAppService :
        MultiStoreReadOnlyAppService<ProductView, ProductViewDto, Guid, GetProductListInput>,
        IProductViewAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;
        protected override string CrossStorePolicyName { get; set; } = ProductsPermissions.Products.CrossStore;

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

        protected override IQueryable<ProductView> ApplyDefaultSorting(IQueryable<ProductView> query)
        {
            return query.OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Id);
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

            var now = Clock.Now;

            using var uow = UnitOfWorkManager.Begin(true, true);

            await _repository.DeleteAsync(x => x.StoreId == storeId, true);

            var productViews = new List<ProductView>();

            foreach (var product in products)
            {
                var productView = ObjectMapper.Map<Product, ProductView>(product);

                await FillPriceInfoWithRealPriceAsync(product, productView, now);

                productViews.Add(await _repository.InsertAsync(productView));
            }

            await uow.SaveChangesAsync();
            await uow.CompleteAsync();

            var duration = await GetCacheDurationOrNullAsync(productViews, now);

            if (duration.HasValue)
            {
                await _cache.SetAsync(await GetCacheKeyAsync(storeId), new ProductViewCacheItem(),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = duration
                    });
            }
        }

        protected async Task<TimeSpan?> GetCacheDurationOrNullAsync(List<ProductView> productViews,
            DateTime now)
        {
            // refresh the cache when a new discount takes effect or an old discount ends.
            var minFromTime = productViews.SelectMany(x => x.ProductDiscounts)
                .Where(x => x.FromTime.HasValue && x.FromTime > now).MinBy(x => x.FromTime)?.FromTime;
            var minToTime = productViews.SelectMany(x => x.ProductDiscounts)
                .Where(x => x.ToTime.HasValue && x.ToTime > now).MinBy(x => x.ToTime)?.ToTime;

            DateTime? nextTime;
            if (minFromTime.HasValue)
            {
                if (minToTime.HasValue)
                {
                    nextTime = minFromTime < minToTime ? minFromTime : minToTime;
                }
                else
                {
                    nextTime = minFromTime;
                }
            }
            else
            {
                nextTime = minToTime;
            }

            // use `Clock.Now` to achieve higher timeliness.
            var duration = nextTime.HasValue ? nextTime - Clock.Now : null;

            if (duration <= TimeSpan.Zero)
            {
                return null;
            }

            var defaultDuration = TimeSpan.FromSeconds(Convert.ToInt32(
                await SettingProvider.GetOrNullAsync(ProductsSettings.ProductView.CacheDurationSeconds)));

            return duration.HasValue && duration.Value < defaultDuration ? duration : defaultDuration;
        }

        protected virtual async Task FillPriceInfoWithRealPriceAsync(Product product, ProductView productView,
            DateTime now)
        {
            if (product.ProductSkus.IsNullOrEmpty())
            {
                return;
            }

            decimal? min = null, max = null;
            decimal? minWithoutDiscount = null, maxWithoutDiscount = null;

            var discounts = new DiscountsInfoModel();

            foreach (var productSku in product.ProductSkus)
            {
                var overrideProductDiscounts = false;
                var priceDataModel = await _productManager.GetRealPriceAsync(product, productSku, now);

                if (min is null || priceDataModel.DiscountedPrice < min.Value)
                {
                    min = priceDataModel.DiscountedPrice;
                    overrideProductDiscounts = true;
                }

                if (max is null || priceDataModel.DiscountedPrice > max.Value)
                {
                    max = priceDataModel.DiscountedPrice;
                }

                if (minWithoutDiscount is null || priceDataModel.PriceWithoutDiscount < minWithoutDiscount.Value)
                {
                    minWithoutDiscount = priceDataModel.PriceWithoutDiscount;
                }

                if (maxWithoutDiscount is null || priceDataModel.PriceWithoutDiscount > maxWithoutDiscount.Value)
                {
                    maxWithoutDiscount = priceDataModel.PriceWithoutDiscount;
                }

                foreach (var model in priceDataModel.ProductDiscounts)
                {
                    var discount = discounts.FindProductDiscount(model.Name, model.Key);

                    if (discount is null || overrideProductDiscounts)
                    {
                        discounts.AddOrUpdateProductDiscount(new ProductDiscountInfoModel(model.Name, model.Key,
                            model.DisplayName, model.DiscountedAmount, model.FromTime, model.ToTime));
                    }
                }

                foreach (var model in priceDataModel.OrderDiscountPreviews)
                {
                    var discount = discounts.FindOrderDiscount(model.Name, model.Key);

                    if (discount is null)
                    {
                        discounts.AddOrUpdateOrderDiscountPreview(new OrderDiscountPreviewInfoModel(model.Name,
                            model.Key, model.DisplayName, model.MinDiscountedAmount, model.MaxDiscountedAmount,
                            model.FromTime, model.ToTime));
                    }
                }
            }

            productView.SetPrices(min, max, minWithoutDiscount, maxWithoutDiscount);
            productView.SetDiscounts(discounts);
        }
    }
}