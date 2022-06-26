using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesResults.FlashSalesResult.ViewModels;

public class ViewFlashSalesResultViewModel
{
    [Display(Name = "FlashSalesResultStoreId")]
    public virtual Guid StoreId { get; protected set; }

    [Display(Name = "FlashSalesResultPlanId")]
    public virtual Guid PlanId { get; protected set; }

    [Display(Name = "FlashSalesResultStatus")]
    public virtual FlashSalesResultStatus Status { get; protected set; }

    [Display(Name = "FlashSalesResultUserId")]
    public virtual Guid UserId { get; protected set; }

    [Display(Name = "FlashSalesResultOrderId")]
    public virtual Guid? OrderId { get; protected set; }
}
