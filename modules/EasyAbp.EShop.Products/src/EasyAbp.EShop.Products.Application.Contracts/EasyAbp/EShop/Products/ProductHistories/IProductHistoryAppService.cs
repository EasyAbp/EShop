using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductHistories.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductHistories
{
    public interface IProductHistoryAppService :
        IReadOnlyAppService< 
            ProductHistoryDto, 
            Guid, 
            GetProductHistoryListDto>
    {
        Task<ProductHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime);
    }
}