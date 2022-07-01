using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetCategories;
using EasyAbp.BookingService.PeriodSchemes;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public class ProductAssetCategoryAppService : MultiStoreCrudAppService<ProductAssetCategory, ProductAssetCategoryDto
            , Guid, GetProductAssetCategoryListDto, CreateProductAssetCategoryDto, UpdateProductAssetCategoryDto>,
        IProductAssetCategoryAppService
    {
        protected override string CrossStorePolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Manage;
        protected override string GetPolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Default;
        protected override string GetListPolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Default;
        protected override string CreatePolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Create;
        protected override string UpdatePolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Update;
        protected override string DeletePolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Delete;

        private readonly IProductAppService _productAppService;
        private readonly IPeriodSchemeAppService _periodSchemeAppService;
        private readonly IAssetCategoryAppService _assetCategoryAppService;
        private readonly IProductAssetCategoryRepository _repository;
        private readonly ProductAssetCategoryManager _productAssetCategoryManager;

        public ProductAssetCategoryAppService(
            IProductAppService productAppService,
            IPeriodSchemeAppService periodSchemeAppService,
            IAssetCategoryAppService assetCategoryAppService,
            IProductAssetCategoryRepository repository,
            ProductAssetCategoryManager productAssetCategoryManager) : base(repository)
        {
            _productAppService = productAppService;
            _periodSchemeAppService = periodSchemeAppService;
            _assetCategoryAppService = assetCategoryAppService;
            _repository = repository;
            _productAssetCategoryManager = productAssetCategoryManager;
        }

        protected override async Task<IQueryable<ProductAssetCategory>> CreateFilteredQueryAsync(
            GetProductAssetCategoryListDto input)
        {
            return (await base.CreateFilteredQueryAsync(input))
                .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId)
                .WhereIf(input.ProductId.HasValue, x => x.ProductId == input.ProductId)
                .WhereIf(input.ProductSkuId.HasValue, x => x.ProductSkuId == input.ProductSkuId)
                .WhereIf(input.AssetCategoryId.HasValue, x => x.AssetCategoryId == input.AssetCategoryId)
                .WhereIf(input.PeriodSchemeId.HasValue, x => x.PeriodSchemeId == input.PeriodSchemeId);
        }

        public override async Task<PagedResultDto<ProductAssetCategoryDto>> GetListAsync(
            GetProductAssetCategoryListDto input)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName);

            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<ProductAssetCategoryDto>(
                totalCount,
                entityDtos
            );
        }

        public override async Task<ProductAssetCategoryDto> CreateAsync(CreateProductAssetCategoryDto input)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName);

            await EnsureProductSkuExistAsync(input.StoreId, input.ProductId, input.ProductSkuId);
            await EnsureAssetCategoryExistAsync(input.AssetCategoryId);
            await EnsurePeriodSchemeExistAsync(input.PeriodSchemeId);

            var entity = await MapToEntityAsync(input);

            TryToSetTenantId(entity);

            await Repository.InsertAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        protected virtual async Task EnsureProductSkuExistAsync(Guid storeId, Guid productId, Guid productSkuId)
        {
            var product = await _productAppService.GetAsync(productId);

            if (product.StoreId != storeId)
            {
                throw (new BusinessException(BookingErrorCodes.WrongStoreIdForProduct))
                    .WithData(nameof(storeId), storeId)
                    .WithData(nameof(productId), productId);
            }

            product.GetSkuById(productSkuId);
        }

        protected virtual async Task EnsureAssetCategoryExistAsync(Guid assetCategoryId)
        {
            await _assetCategoryAppService.GetAsync(assetCategoryId);
        }

        protected virtual async Task EnsurePeriodSchemeExistAsync(Guid periodSchemeId)
        {
            await _periodSchemeAppService.GetAsync(periodSchemeId);
        }

        protected override async Task<ProductAssetCategory> MapToEntityAsync(CreateProductAssetCategoryDto input)
        {
            return await _productAssetCategoryManager.CreateAsync(input.StoreId, input.ProductId, input.ProductSkuId,
                input.AssetCategoryId, input.PeriodSchemeId, input.FromTime, input.ToTime, input.Currency, input.Price);
        }

        protected override async Task MapToEntityAsync(UpdateProductAssetCategoryDto input, ProductAssetCategory entity)
        {
            await _productAssetCategoryManager.UpdateAsync(
                entity, input.FromTime, input.ToTime, input.Currency, input.Price);
        }

        public virtual async Task<ProductAssetCategoryDto> CreatePeriodAsync(Guid productAssetCategoryId,
            CreateProductAssetCategoryPeriodDto input)
        {
            var productAssetCategory = await GetEntityByIdAsync(productAssetCategoryId);

            await CheckMultiStorePolicyAsync(productAssetCategory.StoreId, UpdatePolicyName);

            productAssetCategory.AddPeriod(
                ObjectMapper.Map<CreateProductAssetCategoryPeriodDto, ProductAssetCategoryPeriod>(input));

            await _repository.UpdateAsync(productAssetCategory, true);

            return await MapToGetOutputDtoAsync(productAssetCategory);
        }

        public virtual async Task<ProductAssetCategoryDto> UpdatePeriodAsync(Guid productAssetCategoryId, Guid periodId,
            UpdateProductAssetCategoryPeriodDto input)
        {
            var productAssetCategory = await GetEntityByIdAsync(productAssetCategoryId);

            await CheckMultiStorePolicyAsync(productAssetCategory.StoreId, UpdatePolicyName);

            var productAssetCategoryPeriod = productAssetCategory.GetPeriod(periodId);

            ObjectMapper.Map(input, productAssetCategoryPeriod);

            await _repository.UpdateAsync(productAssetCategory, true);

            return await MapToGetOutputDtoAsync(productAssetCategory);
        }

        public virtual async Task<ProductAssetCategoryDto> DeletePeriodAsync(Guid productAssetCategoryId, Guid periodId)
        {
            var productAssetCategory = await GetEntityByIdAsync(productAssetCategoryId);

            await CheckMultiStorePolicyAsync(productAssetCategory.StoreId, UpdatePolicyName);

            productAssetCategory.RemovePeriod(periodId);

            await _repository.UpdateAsync(productAssetCategory, true);

            return await MapToGetOutputDtoAsync(productAssetCategory);
        }
    }
}