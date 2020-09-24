using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplate : FullAuditedAggregateRoot<Guid>, ICouponTemplate, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
    
        /// <summary>
        /// If it is set to null, only cross-store admin users can manage this template.
        /// It is not about the coupon usage scope, but the StoreId property of Scopes should be same as it if it not null.
        /// </summary>
        public virtual Guid? StoreId { get; protected set; }

        public virtual CouponType CouponType { get; protected set; }

        [CanBeNull]
        public virtual string UniqueName { get; protected set; }
        
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        /// <summary>
        /// If both the UsableDuration and the UsableEndTime have values, the earlier time is preferred.
        /// </summary>
        public virtual TimeSpan? UsableDuration { get; protected set; }
        
        public virtual DateTime? UsableBeginTime { get; protected set; }
        
        /// <summary>
        /// If both the UsableDuration and the UsableEndTime have values, the earlier time is preferred.
        /// </summary>
        public virtual DateTime? UsableEndTime { get; protected set; }
        
        public virtual decimal ConditionAmount { get; protected set; }
        
        public virtual decimal DiscountAmount { get; protected set; }
        
        /// <summary>
        /// If it is set to true, the coupon can be used when the total price of all the order lines in the order
        /// with products in scope is greater than or equal to ConditionAmount.
        /// </summary>
        public virtual bool IsCrossProductAllowed { get; protected set; }

        /// <summary>
        /// The coupon can be used for any product of any store if this property is set to true.
        /// </summary>
        public virtual bool IsUnscoped { get; protected set; }
        
        public virtual IEnumerable<ICouponTemplateScope> Scopes { get; protected set; }

        protected CouponTemplate()
        {
        }

        public CouponTemplate(
            Guid id, 
            Guid? tenantId, 
            Guid? storeId, 
            CouponType couponType, 
            string uniqueName, 
            string displayName, 
            string description, 
            TimeSpan? usableDuration, 
            DateTime? usableBeginTime, 
            DateTime? usableEndTime, 
            decimal conditionAmount, 
            decimal discountAmount, 
            bool isCrossProductAllowed, 
            bool isUnscoped, 
            IEnumerable<CouponTemplateScope> scopes
        ) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            CouponType = couponType;
            UniqueName = uniqueName;
            DisplayName = displayName;
            Description = description;
            UsableDuration = usableDuration;
            UsableBeginTime = usableBeginTime;
            UsableEndTime = usableEndTime;
            ConditionAmount = conditionAmount;
            DiscountAmount = discountAmount;
            IsCrossProductAllowed = isCrossProductAllowed;
            IsUnscoped = isUnscoped;
            Scopes = scopes;
        }
    }
}
