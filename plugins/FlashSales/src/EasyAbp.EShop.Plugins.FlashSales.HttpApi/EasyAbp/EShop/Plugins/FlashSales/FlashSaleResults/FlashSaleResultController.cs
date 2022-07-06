using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

[Area(EShopPluginsFlashSalesRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EShopPluginsFlashSalesRemoteServiceConsts.RemoteServiceName)]
[Route("/api/e-shop/plugins/flash-sales/flash-sale-result")]
public class FlashSaleResultController : FlashSalesController, IFlashSaleResultAppService
{
    protected IFlashSaleResultAppService Service { get; }

    public FlashSaleResultController(IFlashSaleResultAppService service)
    {
        Service = service;
    }

    [HttpGet("{id}")]
    public virtual Task<FlashSaleResultDto> GetAsync(Guid id)
    {
        return Service.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<FlashSaleResultDto>> GetListAsync(FlashSaleResultGetListInput input)
    {
        return Service.GetListAsync(input);
    }
}
