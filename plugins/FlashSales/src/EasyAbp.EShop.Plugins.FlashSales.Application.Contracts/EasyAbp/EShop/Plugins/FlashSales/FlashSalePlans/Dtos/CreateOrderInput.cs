using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

public class CreateOrderInput : ExtensibleEntityDto
{
    public string CustomerRemark { get; set; }
}
