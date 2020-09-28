using System;
using System.Collections.Generic;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponData : ICoupon
    {
        public Guid Id { get; set; }
        
        public Guid? TenantId { get; set; }
        
        public Guid CouponTemplateId { get; set; }
        
        public Guid UserId { get; set; }
        
        public Guid? OrderId { get; set; }
        
        public DateTime? ExpirationTime { get; set; }
        
        public DateTime? UsedTime { get; set; }
        
        public decimal? DiscountedAmount { get; set; }
    }
}