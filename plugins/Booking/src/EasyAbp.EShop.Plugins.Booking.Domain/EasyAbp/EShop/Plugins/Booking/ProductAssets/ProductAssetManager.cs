using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets;

public class ProductAssetManager : DomainService
{
    private readonly IProductAssetRepository _repository;

    public ProductAssetManager(IProductAssetRepository repository)
    {
        _repository = repository;
    }

    public virtual async Task<ProductAsset> CreateAsync(Guid storeId, Guid productId, Guid productSkuId, Guid assetId,
        Guid periodSchemeId, DateTime fromTime, DateTime? toTime, decimal? price)
    {
        var id = GuidGenerator.Create();
        
        if (await _repository.ExistConflictAsync(
                id, storeId, productId, productSkuId, assetId, periodSchemeId, fromTime))
        {
            throw new BusinessException(BookingErrorCodes.ConflictingProductAsset);
        }

        return new ProductAsset(GuidGenerator.Create(), CurrentTenant.Id, storeId, productId, productSkuId, assetId,
            periodSchemeId, fromTime, toTime, price, new List<ProductAssetPeriod>());
    }

    public virtual async Task UpdateAsync(ProductAsset entity, DateTime fromTime, DateTime? toTime, decimal? price)
    {
        if (await _repository.ExistConflictAsync(entity.Id, entity.StoreId, entity.ProductId, entity.ProductSkuId,
                entity.AssetId, entity.PeriodSchemeId, fromTime))
        {
            throw new BusinessException(BookingErrorCodes.ConflictingProductAsset);
        }

        entity.Update(fromTime, toTime, price);
    }
}