using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

public class FlashSalePlanPreOrderDto : ExtensibleEntityDto
{
    public DateTime ExpiresTime { get; set; }

    public double ExpiresInSeconds { get; set; }
}
