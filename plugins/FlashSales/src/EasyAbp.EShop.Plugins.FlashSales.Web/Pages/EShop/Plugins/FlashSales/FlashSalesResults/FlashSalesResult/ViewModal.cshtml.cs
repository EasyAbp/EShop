using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesResults.FlashSalesResult.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalesResults.FlashSalesResult;

public class ViewModalModel : FlashSalesPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ViewFlashSalesResultViewModel ViewModel { get; set; }

    protected IFlashSalesResultAppService Service { get; }

    public ViewModalModel(IFlashSalesResultAppService service)
    {
        Service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await Service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<FlashSalesResultDto, ViewFlashSalesResultViewModel>(dto);
    }
}
