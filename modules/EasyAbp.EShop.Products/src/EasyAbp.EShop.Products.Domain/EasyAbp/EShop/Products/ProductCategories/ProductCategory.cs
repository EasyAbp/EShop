using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public class ProductCategory : AuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid CategoryId { get; protected set; }
        
        public virtual Guid ProductId { get; protected set; }
        
        public virtual int DisplayOrder { get; protected set; }

        protected ProductCategory()
        {
        }

        public ProductCategory(
            Guid id,
            Guid? tenantId,
            Guid categoryId,
            Guid productId,
            int displayOrder = 0
        ) :base(id)
        {
            TenantId = tenantId;
            CategoryId = categoryId;
            ProductId = productId;
            DisplayOrder = displayOrder;
        }
    }
}
