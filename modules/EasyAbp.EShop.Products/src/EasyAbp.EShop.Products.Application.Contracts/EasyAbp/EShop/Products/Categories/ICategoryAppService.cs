using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products.Categories
{
    public interface ICategoryAppService :
        ICrudAppService< 
            CategoryDto, 
            Guid, 
            GetCategoryListDto,
            CreateUpdateCategoryDto,
            CreateUpdateCategoryDto>
    {
        Task<PagedResultDto<CategorySummaryDto>> GetSummaryListAsync(GetCategoryListDto input);
    }
}