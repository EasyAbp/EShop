using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductDetailHistories.Dtos
{
    [Serializable]
    public class GetProductDetailHistoryListDto : PagedAndSortedResultRequestDto
    {
        public Guid ProductDetailId { get; set; }
    }
}