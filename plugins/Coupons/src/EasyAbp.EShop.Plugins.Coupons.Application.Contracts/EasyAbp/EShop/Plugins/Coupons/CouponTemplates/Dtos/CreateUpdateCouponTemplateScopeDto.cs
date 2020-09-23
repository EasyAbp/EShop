using System;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos
{
    [Serializable]
    public class CreateUpdateCouponTemplateScopeDto
    {
        public Guid StoreId { get; set; }

        public Guid? ProductId { get; set; }

        public Guid? ProductSkuId { get; set; }
    }
}