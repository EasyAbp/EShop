using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class Coupon : FullAuditedAggregateRoot<Guid>, ICoupon, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid CouponTemplateId { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }
        
        public virtual Guid? OrderId { get; protected set; }

        public virtual DateTime? ExpirationTime { get; protected set; }

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
            DateTime? expirationTime) : base(id)
        {
            TenantId = tenantId;
            CouponTemplateId = couponTemplateId;
            UserId = userId;
            OrderId = orderId;
            ExpirationTime = expirationTime;
        }

        public void SetOrderId(Guid? orderId)
        {
            OrderId = orderId;
        }

        public void SetExpirationTime(DateTime? expirationTime)
        {
            ExpirationTime = expirationTime;
        }

        public void SetUsed(DateTime? usedTime, decimal? discountedAmount)
        {
            UsedTime = usedTime;
            DiscountedAmount = discountedAmount;
        }
    }
}
