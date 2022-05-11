using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets;

/// <summary>
/// Mapping of ProductSku to Asset.
/// Set which SKU can use to book a specified asset.
/// </summary>
public class ProductAsset : AuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }
    
    public virtual Guid ProductId { get; protected set; }
    
    public virtual Guid ProductSkuId { get; protected set; }
    
    public virtual Guid AssetId { get; protected set; }
    
    /// <summary>
    /// Fall back to the price of ProductSku if <c>null</c>.
    /// </summary>
    public virtual decimal? Price { get; protected set; }
}