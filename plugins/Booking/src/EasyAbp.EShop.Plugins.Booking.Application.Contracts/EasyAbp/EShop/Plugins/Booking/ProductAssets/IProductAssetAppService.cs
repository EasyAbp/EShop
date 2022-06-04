using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public interface IProductAssetAppService :
        ICrudAppService< 
            ProductAssetDto, 
            Guid, 
            GetProductAssetListDto,
            CreateProductAssetDto,
            UpdateProductAssetDto>
    {
        Task<ProductAssetDto> CreatePeriodAsync(Guid productAssetId, CreateProductAssetPeriodDto input);

        Task<ProductAssetDto> UpdatePeriodAsync(Guid productAssetId, Guid periodId, UpdateProductAssetPeriodDto input);

        Task<ProductAssetDto> DeletePeriodAsync(Guid productAssetId, Guid periodId);
    }
}