using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

public class OrderFlashSalePlanInput : ExtensibleEntityDto
{
    public string CustomerRemark { get; set; }
}
