using System;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public interface ICoupon
    {
        Guid Id { get; }

        Guid CouponTemplateId { get; }

        Guid UserId { get; }

        Guid? OrderId { get; }

        DateTime? ExpirationTime { get; }

        DateTime? UsedTime { get; }

        decimal? DiscountedAmount { get; }
    }
}