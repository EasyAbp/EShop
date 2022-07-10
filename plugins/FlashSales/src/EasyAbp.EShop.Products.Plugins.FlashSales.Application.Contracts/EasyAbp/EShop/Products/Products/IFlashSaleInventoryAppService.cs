using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products;

public interface IFlashSaleInventoryAppService : IApplicationService
{
    Task<bool> TryReduceAsync(ReduceInventoryInput input);

    Task<bool> TryIncreaseAsync(IncreaseInventoryInput input);
}
