using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetail : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string Description { get; protected set; }

        protected ProductDetail() {}
        
        public ProductDetail(
            Guid id,
            [CanBeNull] string description) : base(id)
        {
            Description = description;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }
    }
}