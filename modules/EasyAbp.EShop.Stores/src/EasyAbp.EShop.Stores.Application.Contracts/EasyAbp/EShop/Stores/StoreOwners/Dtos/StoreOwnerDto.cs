using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    [Serializable]
    public class StoreOwnerDto : ExtensibleFullAuditedEntityDto<Guid>, IMultiStore
    {
        public Guid StoreId { get; set; }

        public Guid OwnerUserId { get; set; }
    }
}