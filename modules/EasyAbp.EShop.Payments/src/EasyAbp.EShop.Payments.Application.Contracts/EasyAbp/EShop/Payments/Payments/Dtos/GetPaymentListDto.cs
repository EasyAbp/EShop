using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Payments.Payments.Dtos
{
    [Serializable]
    public class GetPaymentListDto : PagedAndSortedResultRequestDto
    {
        public Guid? UserId { get; set; }
    }
}