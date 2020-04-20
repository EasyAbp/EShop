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

        protected ProductType()
        {
        }

        public ProductType(
            Guid id,
            string name,
            string displayName,
            string description,
            MultiTenancySides multiTenancySide
        ) :base(id)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            MultiTenancySide = multiTenancySide;
        }
    }
}
