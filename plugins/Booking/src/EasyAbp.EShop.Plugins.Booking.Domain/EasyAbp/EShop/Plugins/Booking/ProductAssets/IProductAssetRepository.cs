using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public interface IProductAssetRepository : IRepository<ProductAsset, Guid>
    {
        Task<ProductAsset> FindEffectiveAsync(DateTime currentTime, Guid storeId, Guid productId, Guid productSkuId,
            Guid assetId, Guid periodSchemeId);

        Task<bool> ExistConflictAsync(Guid id, Guid storeId, Guid productId, Guid productSkuId, Guid assetId,
            Guid periodSchemeId, DateTime fromTime);
    }
}