using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwner : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public virtual Guid OwnerId { get; protected set; }

        protected StoreOwner()
        {
        }

        public StoreOwner(Guid id, Guid storeId, Guid ownerId, Guid? tenantId = null) : base(id)
        {
            StoreId = storeId;
            OwnerId = ownerId;
            TenantId = tenantId;
        }
    }
}
