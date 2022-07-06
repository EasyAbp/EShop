using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan.ViewModels;

public class CreateFlashSalePlanViewModel
{
    [Display(Name = "FlashSalePlanStoreId")]
    public Guid StoreId { get; set; }

    [Display(Name = "FlashSalePlanBeginTime")]
    public DateTime BeginTime { get; set; }

    [Display(Name = "FlashSalePlanEndTime")]
    public DateTime EndTime { get; set; }

    [Display(Name = "FlashSalePlanProductId")]
    public Guid ProductId { get; set; }

    [Display(Name = "FlashSalePlanProductSkuId")]
    public Guid ProductSkuId { get; set; }

    [Display(Name = "FlashSalePlanIsPublished")]
    public bool IsPublished { get; set; }
}
