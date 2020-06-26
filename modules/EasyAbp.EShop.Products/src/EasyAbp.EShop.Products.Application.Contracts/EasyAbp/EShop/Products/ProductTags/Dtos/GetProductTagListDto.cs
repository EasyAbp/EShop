using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductTags.Dtos
{
    public class GetProductTagListDto : PagedAndSortedResultRequestDto
    {
        public Guid? TagId { get; set; }

        public Guid? ProductId { get; set; }
    }
}