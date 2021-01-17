using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductDetails.Dtos
{
    public class GetProductDetailListInput : PagedAndSortedResultRequestDto
    {
        public Guid? StoreId { get; set; }
    }
}