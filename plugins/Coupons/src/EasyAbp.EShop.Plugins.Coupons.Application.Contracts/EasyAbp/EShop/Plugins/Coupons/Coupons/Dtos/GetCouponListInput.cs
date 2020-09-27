using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class GetCouponListInput : PagedAndSortedResultRequestDto
    {
        public bool AvailableOnly { get; set; }
        
        public Guid? UserId { get; set; }
    }
}