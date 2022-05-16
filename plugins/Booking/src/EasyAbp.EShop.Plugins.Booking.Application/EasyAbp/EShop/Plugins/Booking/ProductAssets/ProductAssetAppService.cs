using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public class ProductAssetAppService : CrudAppService<ProductAsset, ProductAssetDto, Guid, PagedAndSortedResultRequestDto, CreateProductAssetDto, UpdateProductAssetDto>,
        IProductAssetAppService
    {
        protected override string GetPolicyName { get; set; } = BookingPermissions.ProductAsset.Default;
        protected override string GetListPolicyName { get; set; } = BookingPermissions.ProductAsset.Default;
        protected override string CreatePolicyName { get; set; } = BookingPermissions.ProductAsset.Create;
        protected override string UpdatePolicyName { get; set; } = BookingPermissions.ProductAsset.Update;
        protected override string DeletePolicyName { get; set; } = BookingPermissions.ProductAsset.Delete;

        private readonly IProductAssetRepository _repository;

        public ProductAssetAppService(IProductAssetRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public virtual async Task<ProductAssetDto> CreatePeriodAsync(Guid productAssetId, CreateProductAssetPeriodDto input)
        {
            await CheckUpdatePolicyAsync();
            
            var productAsset = await GetEntityByIdAsync(productAssetId);

            productAsset.AddPeriod(ObjectMapper.Map<CreateProductAssetPeriodDto, ProductAssetPeriod>(input));

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }

        public virtual async Task<ProductAssetDto> UpdatePeriodAsync(Guid productAssetId, Guid periodId, UpdateProductAssetPeriodDto input)
        {
            await CheckUpdatePolicyAsync();
            
            var productAsset = await GetEntityByIdAsync(productAssetId);

            var productAssetPeriod = productAsset.GetPeriod(periodId);
            
            ObjectMapper.Map(input, productAssetPeriod);

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }

        public virtual async Task<ProductAssetDto> DeletePeriodAsync(Guid productAssetId, Guid periodId)
        {
            await CheckUpdatePolicyAsync();
            
            var productAsset = await GetEntityByIdAsync(productAssetId);

            productAsset.RemovePeriod(periodId);

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }
    }
}
