using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributeOption : FullAuditedEntity<Guid>
    {
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
    }
}