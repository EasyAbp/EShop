using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan;

public class EditModalModel : FlashSalesPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EditFlashSalePlanViewModel ViewModel { get; set; }

    protected IFlashSalePlanAppService Service { get; }

    public EditModalModel(IFlashSalePlanAppService service)
    {
        Service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await Service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<FlashSalePlanDto, EditFlashSalePlanViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<EditFlashSalePlanViewModel, FlashSalePlanUpdateDto>(ViewModel);
        await Service.UpdateAsync(Id, dto);
        return NoContent();
    }
}