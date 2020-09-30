using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public interface ICouponTemplate
    {
        Guid? StoreId { get; }

        CouponType CouponType { get; }

        [CanBeNull]
        string UniqueName { get; }
        
        [NotNull]
        string DisplayName { get; }
        
        [CanBeNull]
        string Description { get; }
        
        TimeSpan? UsableDuration { get; }
        
        DateTime? UsableBeginTime { get; }
        
        DateTime? UsableEndTime { get; }
        
        decimal ConditionAmount { get; }
        
        decimal DiscountAmount { get; }
        
        [NotNull]
        string Currency { get; }
        
        bool IsUnscoped { get; }
    }
}