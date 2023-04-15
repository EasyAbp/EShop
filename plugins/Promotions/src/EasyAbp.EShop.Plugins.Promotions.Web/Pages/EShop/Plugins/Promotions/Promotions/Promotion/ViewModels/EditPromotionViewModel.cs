using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion.ViewModels;

public class EditPromotionViewModel
{
    [Required]
    [ReadOnlyInput]
    [Display(Name = "PromotionPromotionType")]
    [SelectItems(nameof(PromotionEntityPageModelBase.PromotionTypeSelectListItems))]
    public string PromotionType { get; set; }

    [Required]
    [Display(Name = "PromotionDisplayName")]
    public string DisplayName { get; set; }

    [Required]
    [Display(Name = "PromotionConfigurations")]
    [TextArea(Rows = 5)]
    public string Configurations { get; set; }

    [Display(Name = "PromotionFromTime")]
    public DateTime? FromTime { get; set; }

    [Display(Name = "PromotionToTime")]
    public DateTime? ToTime { get; set; }

    [Display(Name = "PromotionDisabled")]
    public bool Disabled { get; set; }

    [Display(Name = "PromotionPriority")]
    public int Priority { get; set; }
}