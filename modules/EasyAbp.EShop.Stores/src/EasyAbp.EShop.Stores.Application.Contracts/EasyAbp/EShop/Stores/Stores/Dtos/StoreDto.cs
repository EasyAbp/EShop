using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    public class StoreDto : ExtensibleAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}