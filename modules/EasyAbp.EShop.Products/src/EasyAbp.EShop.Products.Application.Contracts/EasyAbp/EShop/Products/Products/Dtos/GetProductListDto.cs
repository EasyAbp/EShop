using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        public Guid StoreId { get; set; }
        
        public Guid? CategoryId { get; set; }
    }
}