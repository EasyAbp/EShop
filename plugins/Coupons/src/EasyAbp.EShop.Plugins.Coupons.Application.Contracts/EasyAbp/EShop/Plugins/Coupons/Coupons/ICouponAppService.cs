using System;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public interface ICouponAppService :
        ICrudAppService< 
            CouponDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateCouponDto,
            CreateUpdateCouponDto>
    {

    }
}