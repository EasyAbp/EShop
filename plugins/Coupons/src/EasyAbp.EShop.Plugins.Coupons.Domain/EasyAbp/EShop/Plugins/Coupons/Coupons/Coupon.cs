using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class Coupon : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid CouponTemplateId { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }
        
        public virtual Guid? OrderId { get; protected set; }
        
        public virtual DateTime? UsableBeginTime { get; protected set; }
        
        public virtual DateTime? UsableEndTime { get; protected set; }
        
        public virtual DateTime? UsedTime { get; protected set; }
        
        public virtual decimal? DiscountedAmount { get; protected set; }

        protected Coupon()
        {
        }

        public Coupon(
            Guid id, 
            Guid? tenantId, 
            Guid couponTemplateId, 
            Guid userId, 
            Guid? orderId, 
            DateTime? usableBeginTime, 
            DateTime? usableEndTime, 
            DateTime? usedTime, 
            decimal? discountedAmount
        ) : base(id)
        {
            TenantId = tenantId;
            CouponTemplateId = couponTemplateId;
            UserId = userId;
            OrderId = orderId;
            UsableBeginTime = usableBeginTime;
            UsableEndTime = usableEndTime;
            UsedTime = usedTime;
            DiscountedAmount = discountedAmount;
        }
    }
}
