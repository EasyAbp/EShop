using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

public class CreateOrderDto : ExtensibleEntityDto
{
    public bool IsSuccess { get; set; }

    public Guid? FlashSaleResultId { get; set; }
}
