using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    public class StoreOwnerDto : AuditedEntityDto<Guid>
    {
        public Guid StoreId { get; set; }

        public Guid OwnerId { get; set; }
    }
}