using EasyAbp.EShop.Inventory.Inventories;
using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.Suppliers.Dtos
{
    [Serializable]
    public class SupplierDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public Address Address { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactPhoneNumber { get; set; }

        public string BusinessScope { get; set; }

    }
}