using System;
namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class CreateCouponDto
    {
        public Guid CouponTemplateId { get; set; }

        public Guid UserId { get; set; }
    }
}