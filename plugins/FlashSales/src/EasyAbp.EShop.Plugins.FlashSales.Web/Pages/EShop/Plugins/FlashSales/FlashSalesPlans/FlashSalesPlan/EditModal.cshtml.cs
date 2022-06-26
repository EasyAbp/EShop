using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesPlans.FlashSalesPlan.ViewModels;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesPlans.FlashSalesPlan;

public class EditModalModel : FlashSalesPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EditFlashSalesPlanViewModel ViewModel { get; set; }

    protected IFlashSalesPlanAppService Service { get; }

    public EditModalModel(IFlashSalesPlanAppService service)
    {
        Service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await Service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<FlashSalesPlanDto, EditFlashSalesPlanViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<EditFlashSalesPlanViewModel, FlashSalesPlanUpdateDto>(ViewModel);
        await Service.UpdateAsync(Id, dto);
        return NoContent();
    }
}