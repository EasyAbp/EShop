using System;
using System.ComponentModel;
namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class CreateUpdateCouponDto
    {
        public Guid CouponTemplateId { get; set; }

        public Guid UserId { get; set; }
    }
}