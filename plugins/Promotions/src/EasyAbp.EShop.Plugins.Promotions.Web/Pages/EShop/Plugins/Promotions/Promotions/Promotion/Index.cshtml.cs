using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion;

public class IndexModel : PromotionEntityPageModelBase
{
    public PromotionFilterInput? PromotionFilter { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid? StoreId { get; set; }

    private readonly IStoreAppService _storeAppService;

    public string? StoreName { get; set; }

    public IndexModel(IStoreAppService storeAppService)
    {
        _storeAppService = storeAppService;
    }

    protected override async Task InternalOnGetAsync()
    {
        if (StoreId.HasValue)
        {
            StoreName = (await _storeAppService.GetAsync(StoreId.Value)).Name;
        }

        PromotionTypes = (await Service.GetPromotionTypesAsync()).Items.ToDictionary(x => x.Name);

        PromotionTypeSelectListItems =
            PromotionTypes.Values.Select(x => new SelectListItem(x.DisplayName, x.Name)).ToList();
    }
}

public class PromotionFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "PromotionStoreId")]
    public Guid? StoreId { get; set; }

    [SelectItems(nameof(PromotionEntityPageModelBase.PromotionTypeSelectListItems))]
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "PromotionPromotionType")]
    public string? PromotionType { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "PromotionUniqueName")]
    public string? UniqueName { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "PromotionDisplayName")]
    public string? DisplayName { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "PromotionFromTime")]
    public DateTime? FromTime { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "PromotionToTime")]
    public DateTime? ToTime { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "PromotionDisabled")]
    public bool? Disabled { get; set; }
}