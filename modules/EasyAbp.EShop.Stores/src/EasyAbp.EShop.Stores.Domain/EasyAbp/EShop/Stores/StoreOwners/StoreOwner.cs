using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwner : FullAuditedAggregateRoot<Guid>, IMultiTenant, IMultiStore
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid StoreId { get; protected set; }

        public virtual Guid OwnerUserId { get; protected set; }

        protected StoreOwner()
        {
        }

        public StoreOwner(Guid id, Guid storeId, Guid ownerUserId, Guid? tenantId = null) : base(id)
        {
            StoreId = storeId;
            OwnerUserId = ownerUserId;
            TenantId = tenantId;
        }
    }
}
