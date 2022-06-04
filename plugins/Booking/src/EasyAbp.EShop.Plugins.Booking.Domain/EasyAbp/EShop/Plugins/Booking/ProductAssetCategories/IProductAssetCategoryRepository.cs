using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public interface IProductAssetCategoryRepository : IRepository<ProductAssetCategory, Guid>
    {
        Task<ProductAssetCategory> FindEffectiveAsync(DateTime currentTime, Guid storeId, Guid productId,
            Guid productSkuId, Guid assetCategoryId, Guid periodSchemeId);

        Task<bool> ExistConflictAsync(Guid id, Guid storeId, Guid productId, Guid productSkuId, Guid assetCategoryId,
            Guid periodSchemeId, DateTime fromTime);
    }
}