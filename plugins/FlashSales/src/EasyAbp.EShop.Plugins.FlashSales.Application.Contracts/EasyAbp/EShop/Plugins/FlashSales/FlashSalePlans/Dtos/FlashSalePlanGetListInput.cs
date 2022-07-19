using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

[Serializable]
public class FlashSalePlanGetListInput : ExtensiblePagedAndSortedResultRequestDto
{
    public Guid? StoreId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? ProductSkuId { get; set; }

    public bool IncludeUnpublished { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }
}