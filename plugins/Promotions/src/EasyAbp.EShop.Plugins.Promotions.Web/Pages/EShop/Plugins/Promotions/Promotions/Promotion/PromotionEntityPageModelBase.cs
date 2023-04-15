using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyAbp.EShop.Plugins.Promotions.Web.Pages.EShop.Plugins.Promotions.Promotions.Promotion;

public abstract class PromotionEntityPageModelBase : PromotionsPageModel
{
    public List<SelectListItem> PromotionTypeSelectListItems { get; set; }
    public Dictionary<string, PromotionTypeDto> PromotionTypes { get; set; }

    protected IPromotionAppService Service => LazyServiceProvider.LazyGetRequiredService<IPromotionAppService>();

    public virtual async Task OnGetAsync()
    {
        PromotionTypes = (await Service.GetPromotionTypesAsync()).Items.ToDictionary(x => x.Name);

        PromotionTypeSelectListItems =
            PromotionTypes.Values.Select(x => new SelectListItem(x.DisplayName, x.Name)).ToList();

        await InternalOnGetAsync();
    }

    protected abstract Task InternalOnGetAsync();
}