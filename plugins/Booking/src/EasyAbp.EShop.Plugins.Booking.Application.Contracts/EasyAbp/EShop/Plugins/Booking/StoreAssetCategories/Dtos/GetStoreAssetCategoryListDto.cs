using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;

[Serializable]
public class GetStoreAssetCategoryListDto : PagedAndSortedResultRequestDto
{
    public Guid? StoreId { get; set; }

    public Guid? AssetCategoryId { get; set; }
}