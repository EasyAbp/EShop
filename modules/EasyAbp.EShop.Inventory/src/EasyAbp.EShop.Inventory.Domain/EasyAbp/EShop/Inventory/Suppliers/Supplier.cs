
using EasyAbp.EShop.Inventory.Inventories;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    public class Supplier : FullAuditedAggregateRoot<Guid>, ISupplier, IMultiTenant
    {

        public string Name { get; protected set; }

        public Address Address { get; protected set; }

        public string Description { get; protected set; }

        public string ContactName { get; protected set; }

        public string ContactPhoneNumber { get; protected set; }

        public string BusinessScope { get; protected set; }

        public virtual Guid? TenantId { get; protected set; }


        protected Supplier()
        {
        }

        public Supplier(
            Guid id,
            string name,
            Address address,
            string description,
            string contactName,
            string contactPhoneNumber,
            string businessScope,
            Guid? tenantId
        ) : base(id)
        {
            Name = name;
            Address = address;
            Description = description;
            ContactName = contactName;
            ContactPhoneNumber = contactPhoneNumber;
            BusinessScope = businessScope;
            TenantId = tenantId;
        }
    }
}
