using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;

[Serializable]
public class GetGrantedStoreListDto : PagedAndSortedResultRequestDto
{
    public Guid? StoreId { get; set; }

    public Guid? AssetId { get; set; }

    public Guid? AssetCategoryId { get; set; }

    public bool? AllowAll { get; set; }
}