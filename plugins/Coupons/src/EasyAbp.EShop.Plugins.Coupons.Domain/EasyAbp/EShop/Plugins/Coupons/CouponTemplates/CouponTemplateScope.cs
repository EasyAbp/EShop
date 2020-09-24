using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateScope : FullAuditedEntity<Guid>, ICouponTemplateScope
    {
        public virtual Guid StoreId { get; protected set; }

        /// <summary>
        /// Specified product group can use the coupon if this property is set.
        /// </summary>
        [CanBeNull]
        public virtual string ProductGroupName { get; protected set; }
        
        /// <summary>
        /// Specified product can use the coupon if this property is set.
        /// </summary>
        public virtual Guid? ProductId { get; protected set; }
        
        /// <summary>
        /// Specified product SKU can use the coupon if this property is set.
        /// </summary>
        public virtual Guid? ProductSkuId { get; protected set; }
    }
}