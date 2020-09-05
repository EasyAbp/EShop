using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Categories.Dtos
{
    [Serializable]
    public class GetCategoryListDto : PagedAndSortedResultRequestDto
    {
        public bool ShowHidden { get; set; }
    }
}