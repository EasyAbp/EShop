using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.Coupons.Coupon.ViewModels
{
    public class CreateCouponViewModel
    {
        [Display(Name = "CouponCouponTemplateId")]
        public Guid CouponTemplateId { get; set; }

        [Display(Name = "CouponUserId")]
        public Guid UserId { get; set; }
    }
}