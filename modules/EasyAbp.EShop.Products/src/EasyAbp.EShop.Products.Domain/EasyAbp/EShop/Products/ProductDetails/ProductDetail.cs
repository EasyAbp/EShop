using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetail : FullAuditedAggregateRoot<Guid>
    {
        public virtual Guid? StoreId { get; protected set; }

        [CanBeNull]
        public virtual string Description { get; protected set; }

        protected ProductDetail() {}
        
        public ProductDetail(
            Guid id,
            Guid? storeId,
            [CanBeNull] string description) : base(id)
        {
            StoreId = storeId;
            Description = description;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}