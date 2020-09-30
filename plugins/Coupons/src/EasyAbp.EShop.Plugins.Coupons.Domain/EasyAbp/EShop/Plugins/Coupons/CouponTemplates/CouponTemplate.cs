using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplate : FullAuditedAggregateRoot<Guid>, ICouponTemplate, IHasCouponTemplateScopes<CouponTemplateScope>, IMultiTenant
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
        
        public virtual string Currency { get; protected set; }

        /// <summary>
        /// The coupon can be used for any product of any store if this property is set to true.
        /// </summary>
        public virtual bool IsUnscoped { get; protected set; }
        
        public virtual List<CouponTemplateScope> Scopes { get; protected set; }

        protected CouponTemplate()
        {
            Scopes = new List<CouponTemplateScope>();
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
            [NotNull] string currency,
            bool isUnscoped, 
            List<CouponTemplateScope> scopes
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
            Currency = currency;
            IsUnscoped = isUnscoped;
            Scopes = scopes ?? new List<CouponTemplateScope>();
        }

        public DateTime? GetCalculatedExpirationTime(IClock clock)
        {
            DateTime? expirationTime = null;

            if (UsableDuration.HasValue)
            {
                expirationTime = clock.Now + UsableDuration;
            }

            if (UsableEndTime.HasValue && (expirationTime == null || UsableEndTime.Value < expirationTime))
            {
                expirationTime = UsableEndTime;
            }

            return expirationTime;
        }
    }
}
