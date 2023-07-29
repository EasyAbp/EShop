using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Refunds.Dtos
{
    [Serializable]
    public class GetRefundListDto : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }

        public Guid? PaymentId { get; set; }
    }
}