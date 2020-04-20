using System;
using EasyAbp.EShop.Products.Categories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Categories
{
    public interface ICategoryAppService :
        ICrudAppService< 
            CategoryDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateCategoryDto,
            CreateUpdateCategoryDto>
    {

    }
}