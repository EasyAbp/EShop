using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public class Promotion : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMultiStore
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual Guid StoreId { get; protected set; }

    /// <summary>
    /// Value of the promotion type name. It is used as the DiscountName (with EffectGroup == `Promotion`)
    /// </summary>
    /// <example>SimpleProductDiscount</example>
    public virtual string PromotionType { get; protected set; }

    /// <summary>
    /// An immutable and unique name for the promotion. It is used as the DiscountKey (with EffectGroup == `Promotion`).
    /// </summary>
    public virtual string UniqueName { get; protected set; }

    public virtual string DisplayName { get; protected set; }

    /// <summary>
    /// The promotion type-specific serialized configurations value.
    /// </summary>
    public virtual string Configurations { get; protected set; }

    public virtual DateTime? FromTime { get; protected set; }

    public virtual DateTime? ToTime { get; protected set; }

    public virtual bool Disabled { get; protected set; }

    public virtual int Priority { get; protected set; }

    protected Promotion()
    {
    }

    internal Promotion(Guid id, Guid? tenantId, Guid storeId, string promotionType, string uniqueName,
        string displayName, string configurations, DateTime? fromTime, DateTime? toTime, bool disabled,
        int priority) : base(id)
    {
        TenantId = tenantId;
        StoreId = storeId;
        PromotionType = promotionType;
        UniqueName = uniqueName;
        DisplayName = displayName;
        Configurations = configurations;
        FromTime = fromTime;
        ToTime = toTime;
        Disabled = disabled;
        Priority = priority;
    }

    internal void Update(string displayName, string configurations, DateTime? fromTime, DateTime? toTime, bool disabled,
        int priority)
    {
        DisplayName = displayName;
        Configurations = configurations;
        FromTime = fromTime;
        ToTime = toTime;
        Disabled = disabled;
        Priority = priority;
    }
}