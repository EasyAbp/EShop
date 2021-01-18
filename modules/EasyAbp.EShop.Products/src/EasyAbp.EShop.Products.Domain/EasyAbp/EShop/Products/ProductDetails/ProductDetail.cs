using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid? StoreId { get; protected set; }

        [CanBeNull]
        public virtual string Description { get; protected set; }

        protected ProductDetail() {}
        
        public ProductDetail(
            Guid id,
            Guid? tenantId,
            Guid? storeId,
            [CanBeNull] string description) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            Description = description;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}