using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesPlans.FlashSalesPlan.ViewModels;

public class CreateFlashSalesPlanViewModel
{
    [Display(Name = "FlashSalesPlanStoreId")]
    public Guid StoreId { get; set; }

    [Display(Name = "FlashSalesPlanBeginTime")]
    public DateTime BeginTime { get; set; }

    [Display(Name = "FlashSalesPlanEndTime")]
    public DateTime EndTime { get; set; }

    [Display(Name = "FlashSalesPlanProductId")]
    public Guid ProductId { get; set; }

    [Display(Name = "FlashSalesPlanProductSkuId")]
    public Guid ProductSkuId { get; set; }

    [Display(Name = "FlashSalesPlanIsPublished")]
    public bool IsPublished { get; set; }
}
