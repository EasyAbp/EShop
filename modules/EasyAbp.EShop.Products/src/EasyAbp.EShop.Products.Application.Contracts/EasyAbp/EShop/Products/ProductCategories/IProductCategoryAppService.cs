using System;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.ProductCategories
{
    public interface IProductCategoryAppService :
        IReadOnlyAppService< 
            ProductCategoryDto, 
            Guid, 
            GetProductCategoryListDto>
    {

    }
}