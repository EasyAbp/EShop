using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

[Area(EShopPluginsFlashSalesRemoteServiceConsts.ModuleName)]
[RemoteService(Name = EShopPluginsFlashSalesRemoteServiceConsts.RemoteServiceName)]
[Route("/api/e-shop/plugins/flash-sales/flash-sale-plan")]
public class FlashSalePlanController :
    FlashSalesController,
    IFlashSalePlanAppService
{
    protected IFlashSalePlanAppService Service { get; }

    public FlashSalePlanController(IFlashSalePlanAppService flashSalePlanAppService)
    {
        Service = flashSalePlanAppService;
    }

    [HttpGet("{id}")]
    public virtual Task<FlashSalePlanDto> GetAsync(Guid id)
    {
        return Service.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<FlashSalePlanDto>> GetListAsync(FlashSalePlanGetListInput input)
    {
        return Service.GetListAsync(input);
    }

    [HttpPost("{id}")]
    public virtual Task<FlashSalePlanDto> CreateAsync(FlashSalePlanCreateDto input)
    {
        return Service.CreateAsync(input);
    }

    [HttpPut("{id}")]
    public virtual Task<FlashSalePlanDto> UpdateAsync(Guid id, FlashSalePlanUpdateDto input)
    {
        return Service.UpdateAsync(id, input);
    }

    [HttpDelete("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return Service.DeleteAsync(id);
    }

    [UnitOfWork(IsDisabled = true)]
    [HttpPost("{id}/pre-order")]
    public virtual Task<FlashSalePlanPreOrderDto> PreOrderAsync(Guid id)
    {
        return Service.PreOrderAsync(id);
    }

    [DisableAuditing]
    [UnitOfWork(IsDisabled = true)]
    [HttpPost("{id}/order")]
    public virtual Task<FlashSaleOrderResultDto> OrderAsync(Guid id, OrderFlashSalePlanInput flashSalePlanInput)
    {
        return Service.OrderAsync(id, flashSalePlanInput);
    }
}
