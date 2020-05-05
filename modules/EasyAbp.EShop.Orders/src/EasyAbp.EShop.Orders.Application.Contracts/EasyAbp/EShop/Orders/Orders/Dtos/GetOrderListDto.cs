using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        public Guid? StoreId { get; set; }
        
        public Guid? CustomerUserId { get; set; }
    }
}