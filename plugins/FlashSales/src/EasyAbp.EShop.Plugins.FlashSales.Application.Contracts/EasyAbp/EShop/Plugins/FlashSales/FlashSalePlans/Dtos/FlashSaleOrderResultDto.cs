using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

public class FlashSaleOrderResultDto : ExtensibleEntityDto
{
    public bool IsSuccess { get; set; }

    [CanBeNull]
    public string ErrorCode { get; set; }

    [CanBeNull]
    public string ErrorMessage { get; set; }
}