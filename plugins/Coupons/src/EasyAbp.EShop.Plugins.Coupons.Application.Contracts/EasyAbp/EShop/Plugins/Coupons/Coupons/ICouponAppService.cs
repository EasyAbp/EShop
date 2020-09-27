using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public interface ICouponAppService :
        ICrudAppService< 
            CouponDto, 
            Guid, 
            GetCouponListInput,
            CreateCouponDto,
            UpdateCouponDto>
    {
        Task<CouponDto> OccupyAsync(Guid id, OccupyCouponInput input);
    }
}