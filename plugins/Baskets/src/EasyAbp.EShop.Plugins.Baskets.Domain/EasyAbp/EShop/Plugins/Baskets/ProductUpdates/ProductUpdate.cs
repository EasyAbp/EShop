using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.Baskets.ProductUpdates
{
    public class ProductUpdate : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid ProductSkuId { get; protected set; }
        
        protected ProductUpdate()
        {
        }

        public ProductUpdate(
            Guid id,
            Guid? tenantId,
            Guid productSkuId) : base(id)
        {
            TenantId = tenantId;
            ProductSkuId = productSkuId;
        }
    }
}
