using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan;

public class CreateModalModel : FlashSalesPageModel
{
    [BindProperty]
    public CreateFlashSalePlanViewModel ViewModel { get; set; }

    protected IFlashSalePlanAppService Service { get; }

    public CreateModalModel(IFlashSalePlanAppService service)
    {
        Service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateFlashSalePlanViewModel, FlashSalePlanCreateDto>(ViewModel);
        await Service.CreateAsync(dto);
        return NoContent();
    }
}