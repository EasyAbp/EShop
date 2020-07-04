using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Baskets.ProductUpdates
{
    public class ProductUpdate : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid ProductSkuId { get; protected set; }
        
        protected ProductUpdate()
        {
        }

        public ProductUpdate(
            Guid id,
            Guid productSkuId) : base(id)
        {
            ProductSkuId = productSkuId;
        }
    }
}
