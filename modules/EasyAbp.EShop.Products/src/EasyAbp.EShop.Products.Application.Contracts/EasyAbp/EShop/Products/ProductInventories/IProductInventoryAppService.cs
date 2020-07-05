using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductInventories
{
    public interface IProductInventoryAppService : IApplicationService
    {
        Task<ProductInventoryDto> GetAsync(Guid productId, Guid productSkuId);
        
        Task<ProductInventoryDto> UpdateAsync(UpdateProductInventoryDto input);
    }
}