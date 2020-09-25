using System;
using System.Collections.Generic;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateData : ICouponTemplate, IHasCouponTemplateScopes<CouponTemplateScopeData>
    {
        public Guid? StoreId { get; set; }

        public CouponType CouponType { get; set; }

        public string UniqueName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public TimeSpan? UsableDuration { get; set; }

        public DateTime? UsableBeginTime { get; set; }

        public DateTime? UsableEndTime { get; set; }

        public decimal ConditionAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public bool IsCrossProductAllowed { get; set; }

        public bool IsUnscoped { get; set; }

        public List<CouponTemplateScopeData> Scopes { get; set; }
    }
}