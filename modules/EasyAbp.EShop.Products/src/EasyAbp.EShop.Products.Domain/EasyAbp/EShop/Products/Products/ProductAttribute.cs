using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttribute : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        public virtual IEnumerable<ProductAttributeOption> ProductAttributeOptions { get; protected set; }
    }
}