using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductCategories.Dtos
{
    [Serializable]
    public class GetProductCategoryListDto : PagedAndSortedResultRequestDto
    {
        public Guid? CategoryId { get; set; }
        
        public Guid? ProductId { get; set; }
    }
}