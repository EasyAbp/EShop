using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    public class StoreOwnerDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}