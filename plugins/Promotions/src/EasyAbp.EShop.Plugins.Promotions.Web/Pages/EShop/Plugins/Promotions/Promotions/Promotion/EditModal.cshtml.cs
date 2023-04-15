using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion;

public class EditModalModel : PromotionEntityPageModelBase
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EditPromotionViewModel ViewModel { get; set; }

    protected override async Task InternalOnGetAsync()
    {
        var dto = await Service.GetAsync(Id);

        ViewModel = ObjectMapper.Map<PromotionDto, EditPromotionViewModel>(dto);

        var beautifiedConfigurations = JToken.Parse(ViewModel.Configurations).ToString(Formatting.Indented);

        ViewModel.Configurations = beautifiedConfigurations;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new UpdatePromotionDto(ViewModel.DisplayName, ViewModel.Configurations, ViewModel.FromTime,
            ViewModel.ToTime, ViewModel.Disabled, ViewModel.Priority);

        await Service.UpdateAsync(Id, dto);

        return NoContent();
    }
}