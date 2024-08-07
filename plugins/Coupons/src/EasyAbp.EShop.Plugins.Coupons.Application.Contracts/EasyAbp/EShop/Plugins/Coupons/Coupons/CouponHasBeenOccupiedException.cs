﻿using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponHasBeenOccupiedException : BusinessException
    {
        public CouponHasBeenOccupiedException() : base(CouponsErrorCodes.CouponHasBeenOccupied)
        {
        }
    }
}