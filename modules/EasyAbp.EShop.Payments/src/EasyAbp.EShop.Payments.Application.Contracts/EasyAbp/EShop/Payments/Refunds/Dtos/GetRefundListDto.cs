using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    public class GetRefundListDto : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }
    }
}