using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products;

public interface IFlashSaleInventoryAppService : IApplicationService
{
    Task<bool> TryReduceInventoryAsync(ReduceInventoryInput input);

    Task<bool> TryIncreaseInventoryAsync(IncreaseInventoryInput input);
}
