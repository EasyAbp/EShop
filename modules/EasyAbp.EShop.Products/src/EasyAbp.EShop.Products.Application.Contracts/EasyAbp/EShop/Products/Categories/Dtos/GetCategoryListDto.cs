using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Categories.Dtos
{
    public class GetCategoryListDto : PagedAndSortedResultRequestDto
    {
        public bool ShowHidden { get; set; }
    }
}