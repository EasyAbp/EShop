using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductHistories.Dtos
{
    public class GetProductHistoryListDto : PagedAndSortedResultRequestDto
    {
        public Guid ProductId { get; set; }
    }
}