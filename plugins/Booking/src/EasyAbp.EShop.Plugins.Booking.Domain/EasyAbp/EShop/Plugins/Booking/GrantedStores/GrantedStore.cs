using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores;

/// <summary>
/// Mapping of Store to Asset or AssetCategory.
/// It determines which Asset or AssetCategory can a store owner set to provide booking service as a product of its store.
/// </summary>
public class GrantedStore : AuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid StoreId { get; protected set; }

    public virtual Guid? AssetId { get; protected set; }

    public virtual Guid? AssetCategoryId { get; protected set; }

    /// <summary>
    /// Allow using any assets in any categories.
    /// </summary>
    public virtual bool AllowAll { get; protected set; }

    protected GrantedStore()
    {
    }

    public GrantedStore(Guid id, Guid? tenantId, Guid storeId, Guid? assetId, Guid? assetCategoryId,
        bool allowAll) : base(id)
    {
        TenantId = tenantId;
        StoreId = storeId;
        AssetId = assetId;
        AssetCategoryId = assetCategoryId;
        AllowAll = allowAll;
    }
}
