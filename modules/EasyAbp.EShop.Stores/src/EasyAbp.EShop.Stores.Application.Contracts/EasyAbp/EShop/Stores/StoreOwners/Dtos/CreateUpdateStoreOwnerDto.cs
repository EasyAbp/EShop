using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Stores.Stores;

namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    public class CreateUpdateStoreOwnerDto : IMultiStore
    {
        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public Guid OwnerUserId { get; set; }
    }
}