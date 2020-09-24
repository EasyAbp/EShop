using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos
{
    [Serializable]
    public class CouponTemplateScopeDto : FullAuditedEntityDto<Guid>
    {
        public Guid StoreId { get; set; }
        
        public string ProductGroupName { get; set; }
        
        public Guid? ProductId { get; set; }

        public Guid? ProductSkuId { get; set; }
    }
}