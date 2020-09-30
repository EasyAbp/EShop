using System;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class CouponDto : FullAuditedEntityDto<Guid>, ICoupon
    {
        public Guid CouponTemplateId { get; set; }

        public Guid UserId { get; set; }

        public Guid? OrderId { get; set; }

        public DateTime? ExpirationTime { get; set; }
        
        public DateTime? UsedTime { get; set; }

        public decimal? DiscountedAmount { get; set; }
        
        public string Currency { get; set; }

        public CouponTemplateDto CouponTemplate { get; set; }
    }
}