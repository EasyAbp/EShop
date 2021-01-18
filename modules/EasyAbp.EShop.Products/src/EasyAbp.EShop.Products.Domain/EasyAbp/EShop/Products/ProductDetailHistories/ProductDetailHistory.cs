using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistory : AggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid ProductDetailId { get; protected set; }
        
        public virtual DateTime ModificationTime { get; protected set; }
        
        [NotNull]
        public virtual string SerializedEntityData { get; protected set; }
        
        protected ProductDetailHistory() {}

        public ProductDetailHistory(
            Guid id,
            Guid? tenantId,
            Guid productDetailId,
            DateTime modificationTime,
            [NotNull] string serializedEntityData) : base(id)
        {
            TenantId = tenantId;
            ProductDetailId = productDetailId;
            ModificationTime = modificationTime;
            SerializedEntityData = serializedEntityData;
        }
    }
}