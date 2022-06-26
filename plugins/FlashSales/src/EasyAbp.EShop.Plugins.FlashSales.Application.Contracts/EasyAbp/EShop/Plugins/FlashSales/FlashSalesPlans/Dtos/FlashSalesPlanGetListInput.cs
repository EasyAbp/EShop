using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;

[Serializable]
public class FlashSalesPlanGetListInput : ExtensiblePagedAndSortedResultRequestDto
{
    public Guid? StoreId { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? ProductSkuId { get; set; }

    public bool OnlyShowPublished { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }
}