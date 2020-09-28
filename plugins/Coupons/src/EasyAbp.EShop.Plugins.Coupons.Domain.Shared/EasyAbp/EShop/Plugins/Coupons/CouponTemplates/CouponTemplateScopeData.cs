using System;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateScopeData : ICouponTemplateScope
    {
        public Guid StoreId { get; set; }
        
        public string ProductGroupName { get; set; }
        
        public Guid? ProductId { get; set; }
        
        public Guid? ProductSkuId { get; set; }
    }
}