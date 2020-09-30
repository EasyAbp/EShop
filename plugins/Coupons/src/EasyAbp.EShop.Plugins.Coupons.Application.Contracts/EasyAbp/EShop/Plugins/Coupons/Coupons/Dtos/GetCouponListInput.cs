using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos
{
    [Serializable]
    public class GetCouponListInput : PagedAndSortedResultRequestDto
    {
        public bool AvailableOnly { get; set; }
        
        public Guid? StoreId { get; set; }
        
        public Guid? UserId { get; set; }
        
        public bool IncludesUsed { get; set; }
        
        public bool IncludesExpired { get; set; }

        public GetCouponListInput()
        {
            MaxMaxResultCount = CouponsConsts.MaxNotExpiredCouponQuantityPerUser;
        }
    }
}