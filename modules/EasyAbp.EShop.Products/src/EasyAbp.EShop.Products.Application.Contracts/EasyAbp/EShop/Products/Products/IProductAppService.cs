using EasyAbp.EShop.Products.Products.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
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
        Task<ProductDto> CreateSkuAsync(Guid productId, CreateProductSkuDto input);

        Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, UpdateProductSkuDto input);

        Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId);
        
        Task<ProductDto> GetByCodeAsync(Guid storeId, string code);

        Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync();
    }
}