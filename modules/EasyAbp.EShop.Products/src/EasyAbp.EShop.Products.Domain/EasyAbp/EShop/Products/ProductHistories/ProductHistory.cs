using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public class ProductHistory : AggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid ProductId { get; protected set; }
        
        public virtual DateTime ModificationTime { get; protected set; }
        
        [NotNull]
        public virtual string SerializedEntityData { get; protected set; }
        
        protected ProductHistory() {}

        public ProductHistory(
            Guid id,
            Guid? tenantId,
            Guid productId,
            DateTime modificationTime,
            [NotNull] string serializedEntityData) : base(id)
        {
            TenantId = tenantId;
            ProductId = productId;
            ModificationTime = modificationTime;
            SerializedEntityData = serializedEntityData;
        }
    }
}