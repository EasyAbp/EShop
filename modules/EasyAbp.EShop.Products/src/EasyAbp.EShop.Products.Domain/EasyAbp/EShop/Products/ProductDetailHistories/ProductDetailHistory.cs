using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public class ProductDetailHistory : AggregateRoot<Guid>
    {
        public virtual Guid ProductDetailId { get; protected set; }
        
        public virtual DateTime ModificationTime { get; protected set; }
        
        [NotNull]
        public virtual string SerializedEntityData { get; protected set; }
        
        protected ProductDetailHistory() {}

        public ProductDetailHistory(
            Guid id,
            Guid productDetailId,
            DateTime modificationTime,
            [NotNull] string serializedEntityData) : base(id)
        {
            ProductDetailId = productDetailId;
            ModificationTime = modificationTime;
            SerializedEntityData = serializedEntityData;
        }
    }
}