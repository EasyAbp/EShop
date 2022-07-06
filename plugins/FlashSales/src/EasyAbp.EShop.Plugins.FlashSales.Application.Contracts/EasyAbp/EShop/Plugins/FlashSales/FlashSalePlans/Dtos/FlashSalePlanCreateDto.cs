using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

[Serializable]
public class FlashSalePlanCreateDto : ExtensibleEntityDto, IMultiStore
{
    public Guid StoreId { get; set; }

    public DateTime BeginTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public bool IsPublished { get; set; }
}
