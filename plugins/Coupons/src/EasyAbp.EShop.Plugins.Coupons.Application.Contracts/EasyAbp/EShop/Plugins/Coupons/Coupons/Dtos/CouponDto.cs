using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class CouponDto : FullAuditedEntityDto<Guid>
    {
        public Guid CouponTemplateId { get; set; }

        public Guid UserId { get; set; }

        public Guid? OrderId { get; set; }

        public DateTime? UsableBeginTime { get; set; }

        public DateTime? UsableEndTime { get; set; }

        public DateTime? UsedTime { get; set; }

        public decimal? DiscountedAmount { get; set; }
    }
}