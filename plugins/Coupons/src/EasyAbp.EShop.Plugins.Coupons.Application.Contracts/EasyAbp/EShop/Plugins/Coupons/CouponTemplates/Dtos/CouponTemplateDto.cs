using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos
{
    [Serializable]
    public class CouponTemplateDto : FullAuditedEntityDto<Guid>, ICouponTemplate, IHasCouponTemplateScopes<CouponTemplateScopeDto>
    {
        public Guid? StoreId { get; set; }

        public CouponType CouponType { get; set; }

        public string UniqueName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public TimeSpan? UsableDuration { get; set; }

        public DateTime? UsableBeginTime { get; set; }

        public DateTime? UsableEndTime { get; set; }

        public decimal ConditionAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public bool IsUnscoped { get; set; }
        
        public List<CouponTemplateScopeDto> Scopes { get; set; }
    }
}