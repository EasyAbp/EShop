using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public interface IProductDetailAppService :
        ICrudAppService< 
            ProductDetailDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateProductDetailDto,
            CreateUpdateProductDetailDto>
    {
        Task DeleteAsync(Guid id, Guid storeId);
    }
}