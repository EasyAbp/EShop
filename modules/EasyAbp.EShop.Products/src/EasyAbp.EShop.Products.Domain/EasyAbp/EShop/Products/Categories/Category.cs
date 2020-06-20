using System;
using System.Collections.Generic;
using EasyAbp.Abp.Trees;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Categories
{
    public class Category : FullAuditedAggregateRoot<Guid>, ITree<Category>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
    
        public virtual Guid? ParentCategoryId { get; protected set; }

        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; } 
        
        public virtual bool IsHidden { get; protected set; }

        #region Properties of ITree

        [NotNull]
        public virtual string DisplayName { get; set; }
        
        [NotNull]
        public virtual string Code { get; set; }
        
        public virtual int Level { get; set; }
        
        public virtual Guid? ParentId { get; set; }
        
        public virtual Category Parent { get; set; }
        
        public virtual ICollection<Category> Children { get; set; }

        #endregion

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
