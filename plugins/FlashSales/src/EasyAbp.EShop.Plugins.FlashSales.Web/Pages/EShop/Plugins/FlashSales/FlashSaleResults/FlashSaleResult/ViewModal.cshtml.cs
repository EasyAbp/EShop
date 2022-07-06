using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSaleResults.FlashSaleResult.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSaleResults.FlashSaleResult;

public class ViewModalModel : FlashSalesPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ViewFlashSaleResultViewModel ViewModel { get; set; }

    protected IFlashSaleResultAppService Service { get; }

    public ViewModalModel(IFlashSaleResultAppService service)
    {
        Service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await Service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<FlashSaleResultDto, ViewFlashSaleResultViewModel>(dto);
    }
}
