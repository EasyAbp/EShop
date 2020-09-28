using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope.ViewModels
{
    public class CreateEditCouponTemplateScopeViewModel
    {
        [Display(Name = "CouponTemplateScopeStoreId")]
        public Guid StoreId { get; set; }

        [Display(Name = "CouponTemplateScopeProductGroupName")]
        public string ProductGroupName { get; set; }

        [Display(Name = "CouponTemplateScopeProductId")]
        public Guid? ProductId { get; set; }

        [Display(Name = "CouponTemplateScopeProductSkuId")]
        public Guid? ProductSkuId { get; set; }
    }
}