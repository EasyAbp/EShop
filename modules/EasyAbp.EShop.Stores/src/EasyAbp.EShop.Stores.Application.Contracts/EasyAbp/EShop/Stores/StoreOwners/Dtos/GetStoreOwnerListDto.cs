using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    [Serializable]
    public class GetStoreOwnerListDto : PagedAndSortedResultRequestDto
    {
        public Guid? StoreId { get; set; }

        public Guid? OwnerUserId { get; set; }
    }
}