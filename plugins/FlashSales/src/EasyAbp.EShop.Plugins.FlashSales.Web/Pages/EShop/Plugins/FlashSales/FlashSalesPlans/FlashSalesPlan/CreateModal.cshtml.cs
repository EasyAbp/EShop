using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesPlans.FlashSalesPlan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesPlans.FlashSalesPlan;

public class CreateModalModel : FlashSalesPageModel
{
    [BindProperty]
    public CreateFlashSalesPlanViewModel ViewModel { get; set; }

    protected IFlashSalesPlanAppService Service { get; }

    public CreateModalModel(IFlashSalesPlanAppService service)
    {
        Service = service;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<CreateFlashSalesPlanViewModel, FlashSalesPlanCreateDto>(ViewModel);
        await Service.CreateAsync(dto);
        return NoContent();
    }
}