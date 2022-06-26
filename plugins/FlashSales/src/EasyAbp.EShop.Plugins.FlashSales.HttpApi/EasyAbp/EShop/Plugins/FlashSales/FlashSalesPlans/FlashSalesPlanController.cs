using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

[Area(EShopPluginsFlashSalesRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EShopPluginsFlashSalesRemoteServiceConsts.RemoteServiceName)]
[Route("/api/e-shop/plugins/flash-sales/flash-sales-plan")]
public class FlashSalesPlanController :
    FlashSalesController,
    IFlashSalesPlanAppService
{
    protected IFlashSalesPlanAppService Service { get; }

    public FlashSalesPlanController(IFlashSalesPlanAppService flashSalesPlanAppService)
    {
        Service = flashSalesPlanAppService;
    }

    [HttpGet("{id}")]
    public virtual Task<FlashSalesPlanDto> GetAsync(Guid id)
    {
        return Service.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<FlashSalesPlanDto>> GetListAsync(FlashSalesPlanGetListInput input)
    {
        return Service.GetListAsync(input);
    }

    [HttpPost("{id}")]
    public virtual Task<FlashSalesPlanDto> CreateAsync(FlashSalesPlanCreateDto input)
    {
        return Service.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public virtual Task<FlashSalesPlanDto> UpdateAsync(Guid id, FlashSalesPlanUpdateDto input)
    {
        return Service.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return Service.DeleteAsync(id);
    }

    [HttpPost("{id}/pre-order")]
    public virtual Task PreOrderAsync(Guid id)
    {
        return Service.PreOrderAsync(id);
    }

    [HttpGet("{id}/check-pre-order")]
    public virtual Task CheckPreOrderAsync(Guid id)
    {
        return Service.CheckPreOrderAsync(id);
    }

    [HttpPost("{id}/order")]
    public virtual Task CreateOrderAsync(Guid id, CreateOrderInput input)
    {
        return Service.CreateOrderAsync(id, input);
    }
}
