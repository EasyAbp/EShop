using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Categories.Dtos
{
    [Serializable]
    public class GetCategoryListDto : PagedAndSortedResultRequestDto
    {
        public Guid? ParentId { get; set; }
        
        public bool ShowHidden { get; set; }
    }
}