using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateScope : FullAuditedEntity<Guid>, IMultiStore
    {
        public virtual Guid StoreId { get; protected set; }

        /// <summary>
        /// If this property is set to null, the coupon can be used for all products in the store.
        /// </summary>
        public virtual Guid? ProductId { get; protected set; }
        
        /// <summary>
        /// If this property is set to null, the coupon can be used for all SKUs in the product.
        /// </summary>
        public virtual Guid? ProductSkuId { get; protected set; }
    }
}