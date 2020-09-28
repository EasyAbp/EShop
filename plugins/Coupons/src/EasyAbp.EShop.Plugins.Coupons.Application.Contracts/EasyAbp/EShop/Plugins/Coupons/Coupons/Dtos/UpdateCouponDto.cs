using System;
namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class UpdateCouponDto
    {
        public DateTime? ExpirationTime { get; set; }
    }
}