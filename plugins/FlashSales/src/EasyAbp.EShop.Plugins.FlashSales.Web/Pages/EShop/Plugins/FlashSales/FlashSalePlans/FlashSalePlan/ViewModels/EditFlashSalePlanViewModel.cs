using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan.ViewModels;

public class EditFlashSalePlanViewModel : IHasConcurrencyStamp
{
    [DisabledInput]
    [ReadOnlyInput]
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

    [HiddenInput]
    public string ConcurrencyStamp { get; set; }
}