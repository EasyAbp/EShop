using System;
using System.Collections.Generic;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets;

/// <summary>
/// Mapping of ProductSku to Asset.
/// Set which SKU can use to book a specified asset.
/// The matched <see cref="ProductAsset"/> with the larger <see cref="FromTime"/> takes precedence.
/// Fall back to <see cref="ProductAssetCategory"/> if the booking asset has no matched <see cref="ProductAsset"/>.
/// </summary>
public class ProductAsset : AuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }
    
    public virtual Guid ProductId { get; protected set; }
    
    public virtual Guid ProductSkuId { get; protected set; }
    
    public virtual Guid AssetId { get; protected set; }
    
    public virtual Guid PeriodSchemeId { get; protected set; }
    
    /// <summary>
    /// When will this mapping start taking effect.
    /// </summary>
    public virtual DateTime FromTime { get; protected set; }
    
    /// <summary>
    /// When will this mapping stop taking effect.
    /// Setting to <c>null</c> means until forever.
    /// </summary>
    public virtual DateTime? ToTime { get; protected set; }
    
    /// <summary>
    /// Price for any period.
    /// Fall back to the price of ProductSku if <c>null</c>.
    /// </summary>
    public virtual decimal? Price { get; protected set; }
    
    /// <summary>
    /// Customize prices for specified periods.
    /// </summary>
    public List<ProductAssetPeriod> Periods { get; protected set; }
}