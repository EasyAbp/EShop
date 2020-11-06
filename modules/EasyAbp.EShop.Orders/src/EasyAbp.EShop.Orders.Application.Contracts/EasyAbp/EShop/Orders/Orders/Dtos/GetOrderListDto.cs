using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    [Serializable]
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        public Guid? StoreId { get; set; }
        
        public Guid? CustomerUserId { get; set; }

        [Range(1, 100)]
        public override int MaxResultCount { get; set; } = DefaultMaxResultCount;
    }
}