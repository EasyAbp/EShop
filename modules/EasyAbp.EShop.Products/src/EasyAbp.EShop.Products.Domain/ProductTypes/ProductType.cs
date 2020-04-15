using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public class ProductType : FullAuditedAggregateRoot<Guid>
    {
        public virtual string Name { get; protected set; }

        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        public virtual MultiTenancySides MultiTenancySide { get; protected set; }
    }
}