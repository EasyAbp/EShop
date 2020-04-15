using System;
using EasyAbp.EShop.Stores.Stores;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMultiStore
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid? StoreId { get; protected set; }
        
        public virtual Guid ProductTypeId { get; protected set; }
        
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        public virtual InventoryStrategy InventoryStrategy { get; protected set; }

        public virtual bool IsPublished { get; protected set; }
        
        [CanBeNull]
        public virtual string MediaResources { get; protected set; }
    }
}