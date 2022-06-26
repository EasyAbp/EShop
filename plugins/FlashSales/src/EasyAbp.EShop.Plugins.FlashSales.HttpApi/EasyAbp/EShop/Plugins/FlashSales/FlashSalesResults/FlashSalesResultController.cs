using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

[Area(EShopPluginsFlashSalesRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EShopPluginsFlashSalesRemoteServiceConsts.RemoteServiceName)]
[Route("/api/e-shop/plugins/flash-sales/flash-sales-result")]
public class FlashSalesResultController : FlashSalesController, IFlashSalesResultAppService
{
    protected IFlashSalesResultAppService Service { get; }

    public FlashSalesResultController(IFlashSalesResultAppService service)
    {
        Service = service;
    }

    [HttpGet("{id}")]
    public virtual Task<FlashSalesResultDto> GetAsync(Guid id)
    {
        return Service.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<FlashSalesResultDto>> GetListAsync(FlashSalesResultGetListInput input)
    {
        return Service.GetListAsync(input);
    }
}
