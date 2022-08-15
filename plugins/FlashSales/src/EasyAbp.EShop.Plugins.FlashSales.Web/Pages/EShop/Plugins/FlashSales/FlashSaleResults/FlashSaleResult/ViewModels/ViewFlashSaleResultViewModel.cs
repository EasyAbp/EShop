using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSaleResults.FlashSaleResult.ViewModels;

public class ViewFlashSaleResultViewModel
{
    [Display(Name = "FlashSaleResultStoreId")]
    public Guid StoreId { get; set; }

    [Display(Name = "FlashSaleResultPlanId")]
    public Guid PlanId { get; set; }

    [Display(Name = "FlashSaleResultStatus")]
    public FlashSaleResultStatus Status { get; set; }

    [Display(Name = "FlashSaleResultReason")]
    public string Reason { get; set; }

    [Display(Name = "FlashSaleResultUserId")]
    public Guid UserId { get; set; }

    [Display(Name = "FlashSaleResultOrderId")]
    public Guid? OrderId { get; set; }

    [Display(Name = "FlashSaleResultReducedInventoryTime")]
    public DateTime ReducedInventoryTime { get; set; }
}
