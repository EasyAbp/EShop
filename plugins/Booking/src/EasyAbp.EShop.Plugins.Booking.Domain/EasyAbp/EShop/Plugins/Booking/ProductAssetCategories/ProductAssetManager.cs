using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;

public class ProductAssetCategoryManager : DomainService
{
    private readonly IProductAssetCategoryRepository _repository;

    public ProductAssetCategoryManager(IProductAssetCategoryRepository repository)
    {
        _repository = repository;
    }

    public virtual async Task<ProductAssetCategory> CreateAsync(Guid storeId, Guid productId, Guid productSkuId, Guid assetCategoryId,
        Guid periodSchemeId, DateTime fromTime, DateTime? toTime, decimal? price)
    {
        var id = GuidGenerator.Create();
        
        if (await _repository.ExistConflictAsync(
                id, storeId, productId, productSkuId, assetCategoryId, periodSchemeId, fromTime))
        {
            throw new BusinessException(BookingErrorCodes.ConflictingProductAssetCategory);
        }

        return new ProductAssetCategory(GuidGenerator.Create(), CurrentTenant.Id, storeId, productId, productSkuId, assetCategoryId,
            periodSchemeId, fromTime, toTime, price, new List<ProductAssetCategoryPeriod>());
    }

    public virtual async Task UpdateAsync(ProductAssetCategory entity, DateTime fromTime, DateTime? toTime, decimal? price)
    {
        if (await _repository.ExistConflictAsync(entity.Id, entity.StoreId, entity.ProductId, entity.ProductSkuId,
                entity.AssetCategoryId, entity.PeriodSchemeId, fromTime))
        {
            throw new BusinessException(BookingErrorCodes.ConflictingProductAssetCategory);
        }

        entity.Update(fromTime, toTime, price);
    }
}