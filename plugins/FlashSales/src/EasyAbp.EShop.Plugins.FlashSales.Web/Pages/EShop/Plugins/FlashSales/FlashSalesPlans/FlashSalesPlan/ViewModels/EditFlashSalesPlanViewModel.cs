using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesPlans.FlashSalesPlan.ViewModels;

public class EditFlashSalesPlanViewModel : IHasConcurrencyStamp
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

    [HiddenInput]
    public string ConcurrencyStamp { get; set; }
}