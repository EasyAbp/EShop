using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductStores
{
    public class ProductStore : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMultiStore
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid StoreId { get; protected set; }
        
        public virtual Guid ProductId { get; protected set; }
        
        public virtual bool IsOwner { get; protected set; }
        
        protected ProductStore() {}

        public ProductStore(
            Guid id,
            Guid? tenantId,
            Guid storeId,
            Guid productId,
            bool isOwner) : base(id)
        {
            TenantId = tenantId;
            StoreId = storeId;
            ProductId = productId;
            IsOwner = isOwner;
        }
    }
}