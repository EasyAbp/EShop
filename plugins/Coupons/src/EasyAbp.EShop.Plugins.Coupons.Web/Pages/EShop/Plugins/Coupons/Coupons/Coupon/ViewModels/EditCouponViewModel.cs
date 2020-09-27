using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.Coupons.Coupon.ViewModels
{
    public class EditCouponViewModel
    {
        [Display(Name = "CouponExpirationTime")]
        public DateTime? ExpirationTime { get; set; }
    }
}