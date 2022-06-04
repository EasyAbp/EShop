using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.BookingService.Assets;
using EasyAbp.BookingService.PeriodSchemes;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public class ProductAssetAppService : MultiStoreCrudAppService<ProductAsset, ProductAssetDto
            , Guid, GetProductAssetListDto, CreateProductAssetDto, UpdateProductAssetDto>,
        IProductAssetAppService
    {
        protected override string CrossStorePolicyName { get; set; } = BookingPermissions.ProductAsset.Manage;
        protected override string GetPolicyName { get; set; } = BookingPermissions.ProductAsset.Default;
        protected override string GetListPolicyName { get; set; } = BookingPermissions.ProductAsset.Default;
        protected override string CreatePolicyName { get; set; } = BookingPermissions.ProductAsset.Create;
        protected override string UpdatePolicyName { get; set; } = BookingPermissions.ProductAsset.Update;
        protected override string DeletePolicyName { get; set; } = BookingPermissions.ProductAsset.Delete;

        private readonly IProductAppService _productAppService;
        private readonly IPeriodSchemeAppService _periodSchemeAppService;
        private readonly IAssetAppService _assetAppService;
        private readonly IProductAssetRepository _repository;
        private readonly ProductAssetManager _productAssetManager;

        public ProductAssetAppService(
            IProductAppService productAppService,
            IPeriodSchemeAppService periodSchemeAppService,
            IAssetAppService assetAppService,
            IProductAssetRepository repository,
            ProductAssetManager productAssetManager) : base(repository)
        {
            _productAppService = productAppService;
            _periodSchemeAppService = periodSchemeAppService;
            _assetAppService = assetAppService;
            _repository = repository;
            _productAssetManager = productAssetManager;
        }

        protected override async Task<IQueryable<ProductAsset>> CreateFilteredQueryAsync(GetProductAssetListDto input)
        {
            return (await base.CreateFilteredQueryAsync(input))
                .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId)
                .WhereIf(input.ProductId.HasValue, x => x.ProductId == input.ProductId)
                .WhereIf(input.ProductSkuId.HasValue, x => x.ProductSkuId == input.ProductSkuId)
                .WhereIf(input.AssetId.HasValue, x => x.AssetId == input.AssetId)
                .WhereIf(input.PeriodSchemeId.HasValue, x => x.PeriodSchemeId == input.PeriodSchemeId);
        }

        public override async Task<PagedResultDto<ProductAssetDto>> GetListAsync(
            GetProductAssetListDto input)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName);
            
            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<ProductAssetDto>(
                totalCount,
                entityDtos
            );
        }

        public override async Task<ProductAssetDto> CreateAsync(CreateProductAssetDto input)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName);

            await EnsureProductSkuExistAsync(input.StoreId, input.ProductId, input.ProductSkuId);
            await EnsureAssetExistAsync(input.AssetId);
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

        protected virtual async Task EnsureAssetExistAsync(Guid assetId)
        {
            await _assetAppService.GetAsync(assetId);
        }

        protected virtual async Task EnsurePeriodSchemeExistAsync(Guid periodSchemeId)
        {
            await _periodSchemeAppService.GetAsync(periodSchemeId);
        }

        protected override async Task<ProductAsset> MapToEntityAsync(CreateProductAssetDto input)
        {
            return await _productAssetManager.CreateAsync(input.StoreId, input.ProductId, input.ProductSkuId,
                input.AssetId, input.PeriodSchemeId, input.FromTime, input.ToTime, input.Price);
        }

        protected override async Task MapToEntityAsync(UpdateProductAssetDto input, ProductAsset entity)
        {
            await _productAssetManager.UpdateAsync(entity, input.FromTime, input.ToTime, input.Price);
        }

        public virtual async Task<ProductAssetDto> CreatePeriodAsync(Guid productAssetId,
            CreateProductAssetPeriodDto input)
        {
            var productAsset = await GetEntityByIdAsync(productAssetId);

            await CheckMultiStorePolicyAsync(productAsset.StoreId, UpdatePolicyName);

            productAsset.AddPeriod(
                ObjectMapper.Map<CreateProductAssetPeriodDto, ProductAssetPeriod>(input));

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }

        public virtual async Task<ProductAssetDto> UpdatePeriodAsync(Guid productAssetId, Guid periodId,
            UpdateProductAssetPeriodDto input)
        {
            var productAsset = await GetEntityByIdAsync(productAssetId);

            await CheckMultiStorePolicyAsync(productAsset.StoreId, UpdatePolicyName);

            var productAssetPeriod = productAsset.GetPeriod(periodId);

            ObjectMapper.Map(input, productAssetPeriod);

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }

        public virtual async Task<ProductAssetDto> DeletePeriodAsync(Guid productAssetId, Guid periodId)
        {
            var productAsset = await GetEntityByIdAsync(productAssetId);

            await CheckMultiStorePolicyAsync(productAsset.StoreId, UpdatePolicyName);

            productAsset.RemovePeriod(periodId);

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }
    }
}
