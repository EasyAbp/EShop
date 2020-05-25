using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAppService :
        ICrudAppService< 
            ProductDto, 
            Guid, 
            GetProductListDto,
            CreateUpdateProductDto,
            CreateUpdateProductDto>
    {
        Task DeleteAsync(Guid id, Guid storeId);

        Task<ProductDto> CreateSkuAsync(Guid productId, Guid storeId, CreateProductSkuDto input);

        Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, Guid storeId, UpdateProductSkuDto input);

        Task<ProductDto> GetAsync(Guid id, Guid storeId);

        Task<ProductDto> GetByCodeAsync(string code, Guid storeId);

        Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId, Guid storeId);
    }
}