using System;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public interface IProductCategoryAppService :
        ICrudAppService< 
            ProductCategoryDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateProductCategoryDto,
            CreateUpdateProductCategoryDto>
    {

    }
}