using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;

[Serializable]
public class GetProductAssetDto : PagedAndSortedResultRequestDto
{
    public Guid? StoreId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? ProductSkuId { get; set; }

    public Guid? AssetId { get; set; }

    public Guid? PeriodSchemeId { get; set; }
}