using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesResults.FlashSalesResult.ViewModels;

public class ViewFlashSalesResultViewModel
{
    [Display(Name = "FlashSalesResultStoreId")]
    public Guid StoreId { get; set; }

    [Display(Name = "FlashSalesResultPlanId")]
    public Guid PlanId { get; set; }

    [Display(Name = "FlashSalesResultStatus")]
    public FlashSalesResultStatus Status { get; set; }

    [Display(Name = "FlashSalesResultReason")]
    public string Reason { get; set; }

    [Display(Name = "FlashSalesResultUserId")]
    public Guid UserId { get; set; }

    [Display(Name = "FlashSalesResultOrderId")]
    public Guid? OrderId { get; set; }
}
