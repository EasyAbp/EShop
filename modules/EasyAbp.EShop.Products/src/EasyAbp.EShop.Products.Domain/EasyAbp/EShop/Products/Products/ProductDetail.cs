using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductDetail : Entity
    {
        public virtual Guid ProductId { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }

        protected ProductDetail() {}
        
        public ProductDetail(
            Guid productId,
            [CanBeNull] string description)
        {
            ProductId = productId;
            Description = description;
        }
        
        public override object[] GetKeys()
        {
            return new object[] {ProductId};
        }
    }
}