using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;

public class CreateOrderInput : ExtensibleEntityDto
{
    public string CustomerRemark { get; set; }
}
