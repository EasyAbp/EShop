using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    public class CreateUpdateStoreOwnerDto : ExtensibleObject, IMultiStore
    {
        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Guid OwnerUserId { get; set; }
    }
}