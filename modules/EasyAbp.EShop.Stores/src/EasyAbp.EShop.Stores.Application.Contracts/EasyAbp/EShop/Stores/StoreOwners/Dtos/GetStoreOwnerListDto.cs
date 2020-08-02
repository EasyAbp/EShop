using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners.Dtos
{
    public class GetStoreOwnerListDto : PagedAndSortedResultRequestDto
    {
        public Guid? StoreId { get; set; }

        public Guid? OwnerId { get; set; }
    }
}