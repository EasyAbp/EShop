using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    public class GetPaymentListDto : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }
    }
}