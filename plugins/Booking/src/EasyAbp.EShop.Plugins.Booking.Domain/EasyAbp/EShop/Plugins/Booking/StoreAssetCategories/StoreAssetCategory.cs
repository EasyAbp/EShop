using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories;

/// <summary>
/// Mapping of Store to AssetCategory.
/// It determines which AssetCategory can a store owner set to provide booking service as a product of its store.
/// Stores can use all the sub-categories if you set a parent category for them.
/// </summary>
public class StoreAssetCategory : AuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid StoreId { get; protected set; }

    public virtual Guid AssetCategoryId { get; protected set; }

    protected StoreAssetCategory()
    {
    }

    public StoreAssetCategory(Guid id, Guid? tenantId, Guid storeId, Guid assetCategoryId) : base(id)
    {
        TenantId = tenantId;
        StoreId = storeId;
        AssetCategoryId = assetCategoryId;
    }
}
