using System;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class OccupyCouponInput
    {
        public Guid OrderId { get; set; }
    }
}