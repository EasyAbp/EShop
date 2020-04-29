using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Categories
{
    public class Category : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
    
        public virtual Guid? ParentCategoryId { get; protected set; }

        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; } 
        
        public virtual bool IsHidden { get; protected set; }

        protected Category()
        {
        }

        public Category(
            Guid id,
            Guid? tenantId,
            Guid? parentCategoryId,
            string displayName,
            string description,
            string mediaResources,
            bool isHidden
        ) :base(id)
        {
            TenantId = tenantId;
            ParentCategoryId = parentCategoryId;
            DisplayName = displayName;
            Description = description;
            MediaResources = mediaResources;
            IsHidden = isHidden;
        }
    }
}
