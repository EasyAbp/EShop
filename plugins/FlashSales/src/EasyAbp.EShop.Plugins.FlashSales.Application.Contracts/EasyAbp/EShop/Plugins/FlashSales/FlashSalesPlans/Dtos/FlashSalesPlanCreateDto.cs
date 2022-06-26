using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;

[Serializable]
public class FlashSalesPlanCreateDto : ExtensibleEntityDto
{
    public Guid StoreId { get; set; }

    public DateTime BeginTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public bool IsPublished { get; set; }
}