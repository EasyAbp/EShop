using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public interface IProductAssetCategoryAppService :
        ICrudAppService< 
            ProductAssetCategoryDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateProductAssetCategoryDto,
            UpdateProductAssetCategoryDto>
    {
        Task<ProductAssetCategoryDto> CreatePeriodAsync(Guid productAssetCategoryId, CreateProductAssetCategoryPeriodDto input);

        Task<ProductAssetCategoryDto> UpdatePeriodAsync(Guid productAssetCategoryId, Guid periodId, UpdateProductAssetCategoryPeriodDto input);

        Task<ProductAssetCategoryDto> DeletePeriodAsync(Guid productAssetCategoryId, Guid periodId);
    }
}