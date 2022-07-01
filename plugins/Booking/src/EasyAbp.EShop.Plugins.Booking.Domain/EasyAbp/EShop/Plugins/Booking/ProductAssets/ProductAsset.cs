using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.Shared;
using EasyAbp.EShop.Stores.Stores;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets;

/// <summary>
/// Mapping of ProductSku to Asset.
/// Set which SKU can use to book a specified asset.
/// The matched <see cref="ProductAsset"/> with the larger <see cref="FromTime"/> takes precedence.
/// Fall back to <see cref="ProductAssetCategory"/> if the booking asset has no matched <see cref="ProductAsset"/>.
/// </summary>
public class ProductAsset : AuditedAggregateRoot<Guid>, IMultiStore, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid StoreId { get; protected set; }

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
    /// Should set if the <see cref="Price"/> is not <c>null</c>.
    /// </summary>
    [CanBeNull]
    public virtual string Currency { get; protected set; }

    /// <summary>
    /// Price for any period.
    /// Fall back to the price of ProductSku if <c>null</c>.
    /// </summary>
    public virtual decimal? Price { get; protected set; }

    /// <summary>
    /// Customize prices for specified periods.
    /// </summary>
    public List<ProductAssetPeriod> Periods { get; protected set; }

    protected ProductAsset()
    {
    }

    internal ProductAsset(
        Guid id,
        Guid? tenantId,
        Guid storeId,
        Guid productId,
        Guid productSkuId,
        Guid assetId,
        Guid periodSchemeId,
        DateTime fromTime,
        DateTime? toTime,
        [CanBeNull] string currency,
        decimal? price,
        List<ProductAssetPeriod> periods
    ) : base(id)
    {
        TenantId = tenantId;
        StoreId = storeId;
        ProductId = productId;
        ProductSkuId = productSkuId;
        AssetId = assetId;
        PeriodSchemeId = periodSchemeId;
        FromTime = fromTime;
        ToTime = toTime;
        SetPrice(currency, price);

        Periods = periods ?? new List<ProductAssetPeriod>();
    }

    internal void Update(DateTime fromTime, DateTime? toTime, [CanBeNull] string currency, decimal? price)
    {
        FromTime = fromTime;
        ToTime = toTime;
        SetPrice(currency, price);
    }

    protected void SetPrice([CanBeNull] string currency, decimal? price)
    {
        if (price is not null)
        {
            Check.NotNull(currency, nameof(currency));
        }

        Currency = currency;
        Price = price;
    }

    public void AddPeriod(ProductAssetPeriod productAssetPeriod)
    {
        if (FindPeriod(productAssetPeriod.PeriodId) is not null)
        {
            throw new DuplicatePeriodException(productAssetPeriod.PeriodId);
        }

        Periods.Add(productAssetPeriod);
    }

    public void RemovePeriod(Guid periodId)
    {
        Periods.Remove(GetPeriod(periodId));
    }

    public ProductAssetPeriod GetPeriod(Guid periodId)
    {
        return FindPeriod(periodId) ?? throw new PeriodNotFoundException(periodId);
    }

    public ProductAssetPeriod FindPeriod(Guid periodId)
    {
        return Periods.FirstOrDefault(x => x.PeriodId == periodId);
    }
}