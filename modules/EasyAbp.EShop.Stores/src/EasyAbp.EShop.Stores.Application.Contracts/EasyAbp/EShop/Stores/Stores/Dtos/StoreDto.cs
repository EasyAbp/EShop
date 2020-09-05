using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    [Serializable]
    public class StoreDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        [Required]
        public string Name { get; set; }
    }
}