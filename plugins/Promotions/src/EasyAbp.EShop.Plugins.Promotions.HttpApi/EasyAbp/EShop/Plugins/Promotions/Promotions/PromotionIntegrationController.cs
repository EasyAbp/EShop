using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

[RemoteService(Name = EShopPluginsPromotionsRemoteServiceConsts.RemoteServiceName)]
[Route("/integration-api/e-shop/plugins/promotions/promotion")]
public class PromotionIntegrationController : PromotionsController, IPromotionIntegrationService
{
    private readonly IPromotionIntegrationService _service;

    public PromotionIntegrationController(IPromotionIntegrationService service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("discount-products")]
    public virtual Task<DiscountProductOutputDto> DiscountProductsAsync(DiscountProductInputDto input)
    {
        return _service.DiscountProductsAsync(input);
    }

    [HttpPost]
    [Route("discount-order")]
    public virtual Task<DiscountOrderOutputDto> DiscountOrderAsync(DiscountOrderInputDto input)
    {
        return _service.DiscountOrderAsync(input);
    }
}