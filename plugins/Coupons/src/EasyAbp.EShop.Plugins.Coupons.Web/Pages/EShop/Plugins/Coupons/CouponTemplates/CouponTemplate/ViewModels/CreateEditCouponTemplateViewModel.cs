using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels
{
    public class CreateEditCouponTemplateViewModel
    {
        [Display(Name = "CouponTemplateStoreId")]
        public Guid? StoreId { get; set; }

        [Display(Name = "CouponTemplateCouponType")]
        public CouponType CouponType { get; set; }

        [Display(Name = "CouponTemplateUniqueName")]
        public string UniqueName { get; set; }

        [Display(Name = "CouponTemplateDisplayName")]
        public string DisplayName { get; set; }

        [Display(Name = "CouponTemplateDescription")]
        public string Description { get; set; }

        [Display(Name = "CouponTemplateUsableDuration")]
        public TimeSpan? UsableDuration { get; set; }

        [Display(Name = "CouponTemplateUsableBeginTime")]
        public DateTime? UsableBeginTime { get; set; }

        [Display(Name = "CouponTemplateUsableEndTime")]
        public DateTime? UsableEndTime { get; set; }

        [Display(Name = "CouponTemplateConditionAmount")]
        public decimal ConditionAmount { get; set; }

        [Display(Name = "CouponTemplateDiscountAmount")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "CouponTemplateIsCrossProductAllowed")]
        public bool IsCrossProductAllowed { get; set; }

        [Display(Name = "CouponTemplateIsUnscoped")]
        public bool IsUnscoped { get; set; }
    }
}