using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

[RemoteService(Name = EShopPluginsPromotionsRemoteServiceConsts.RemoteServiceName)]
[Route("/api/e-shop/plugins/promotions/promotion")]
public class PromotionController : PromotionsController, IPromotionAppService
{
    private readonly IPromotionAppService _service;

    public PromotionController(IPromotionAppService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<PromotionDto> GetAsync(Guid id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    public virtual Task<PagedResultDto<PromotionDto>> GetListAsync(PromotionGetListInput input)
    {
        return _service.GetListAsync(input);
    }

    [HttpPost]
    public virtual Task<PromotionDto> CreateAsync(CreatePromotionDto input)
    {
        return _service.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<PromotionDto> UpdateAsync(Guid id, UpdatePromotionDto input)
    {
        return _service.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return _service.DeleteAsync(id);
    }

    [HttpGet]
    [Route("promotion-types")]
    public virtual Task<ListResultDto<PromotionTypeDto>> GetPromotionTypesAsync()
    {
        return _service.GetPromotionTypesAsync();
    }
}