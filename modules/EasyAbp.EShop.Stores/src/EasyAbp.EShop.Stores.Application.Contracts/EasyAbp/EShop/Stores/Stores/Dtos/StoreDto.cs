using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    public class StoreDto : ExtensibleAuditedEntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }
    }
}