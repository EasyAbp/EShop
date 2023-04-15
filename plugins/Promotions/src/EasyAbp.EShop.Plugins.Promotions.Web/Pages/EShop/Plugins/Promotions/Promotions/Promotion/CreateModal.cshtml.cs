using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion;

public class CreateModalModel : PromotionEntityPageModelBase
{
    [BindProperty(SupportsGet = true)]
    public CreatePromotionViewModel ViewModel { get; set; }

    protected override async Task InternalOnGetAsync()
    {
        PromotionTypes = (await Service.GetPromotionTypesAsync()).Items.ToDictionary(x => x.Name);

        PromotionTypeSelectListItems =
            PromotionTypes.Values.Select(x => new SelectListItem(x.DisplayName, x.Name)).ToList();

        var beautifiedConfigurations = JToken.Parse(PromotionTypes[ViewModel.PromotionType].ConfigurationsTemplate)
            .ToString(Formatting.Indented);

        ViewModel.Configurations = beautifiedConfigurations;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreatePromotionDto(ViewModel.StoreId, ViewModel.PromotionType, ViewModel.UniqueName,
            ViewModel.DisplayName, ViewModel.Configurations, ViewModel.FromTime, ViewModel.ToTime, ViewModel.Disabled,
            ViewModel.Priority);

        await Service.CreateAsync(dto);
        return NoContent();
    }
}