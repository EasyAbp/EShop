using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products;

[RemoteService(Name = EShopProductsRemoteServiceConsts.RemoteServiceName)]
[Route("/api/e-shop/products/plugins/flash-sale-inventory")]
public class FlashSaleInventoryController : ProductsController, IFlashSaleInventoryAppService
{
    protected IFlashSaleInventoryAppService AppService { get; }

    public FlashSaleInventoryController(IFlashSaleInventoryAppService appService)
    {
        AppService = appService;
    }

    [HttpPost("try-increase")]
    public virtual Task<bool> TryIncreaseAsync(IncreaseInventoryInput input)
    {
        return AppService.TryIncreaseAsync(input);
    }

    [HttpPost("try-reduce")]
    public virtual Task<bool> TryReduceAsync(ReduceInventoryInput input)
    {
        return AppService.TryReduceAsync(input);
    }
}
