using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetailHistories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    public interface IProductDetailHistoryAppService :
        ICrudAppService< 
            ProductDetailHistoryDto, 
            Guid, 
            GetProductDetailHistoryListDto,
            object,
            object>
    {
        Task<ProductDetailHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime);
    }
}