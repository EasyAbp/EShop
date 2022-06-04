using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;

[Serializable]
public class GetProductAssetCategoryDto : PagedAndSortedResultRequestDto
{
    public Guid? StoreId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? ProductSkuId { get; set; }

    public Guid? AssetCategoryId { get; set; }

    public Guid? PeriodSchemeId { get; set; }
}