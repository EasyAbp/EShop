using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public class ProductAssetCategoryAppService : CrudAppService<ProductAssetCategory, ProductAssetCategoryDto, Guid, PagedAndSortedResultRequestDto, CreateProductAssetCategoryDto, UpdateProductAssetCategoryDto>,
        IProductAssetCategoryAppService
    {
        protected override string GetPolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Default;
        protected override string GetListPolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Default;
        protected override string CreatePolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Create;
        protected override string UpdatePolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Update;
        protected override string DeletePolicyName { get; set; } = BookingPermissions.ProductAssetCategory.Delete;

        private readonly IProductAssetCategoryRepository _repository;

        public ProductAssetCategoryAppService(IProductAssetCategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public virtual async Task<ProductAssetCategoryDto> CreatePeriodAsync(Guid productAssetCategoryId, CreateProductAssetCategoryPeriodDto input)
        {
            await CheckUpdatePolicyAsync();
            
            var productAsset = await GetEntityByIdAsync(productAssetCategoryId);

            productAsset.AddPeriod(ObjectMapper.Map<CreateProductAssetCategoryPeriodDto, ProductAssetCategoryPeriod>(input));

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }

        public virtual async Task<ProductAssetCategoryDto> UpdatePeriodAsync(Guid productAssetCategoryId, Guid periodId, UpdateProductAssetCategoryPeriodDto input)
        {
            await CheckUpdatePolicyAsync();
            
            var productAsset = await GetEntityByIdAsync(productAssetCategoryId);

            var productAssetCategoryPeriod = productAsset.GetPeriod(periodId);
            
            ObjectMapper.Map(input, productAssetCategoryPeriod);

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }

        public virtual async Task<ProductAssetCategoryDto> DeletePeriodAsync(Guid productAssetCategoryId, Guid periodId)
        {
            await CheckUpdatePolicyAsync();
            
            var productAsset = await GetEntityByIdAsync(productAssetCategoryId);

            productAsset.RemovePeriod(periodId);

            await _repository.UpdateAsync(productAsset, true);

            return await MapToGetOutputDtoAsync(productAsset);
        }
    }
}
