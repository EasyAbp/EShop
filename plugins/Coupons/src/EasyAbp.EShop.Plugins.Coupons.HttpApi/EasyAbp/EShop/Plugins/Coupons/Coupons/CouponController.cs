using System;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    [RemoteService(Name = EShopPluginsCouponsRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/e-shop/plugins/coupons/coupon")]
    public class CouponController : CouponsController, ICouponAppService
    {
        private readonly ICouponAppService _service;

        public CouponController(ICouponAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual Task<CouponDto> CreateAsync(CreateCouponDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CouponDto> UpdateAsync(Guid id, UpdateCouponDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpPost]
        [Route("{id}/occupy")]
        public Task<CouponDto> OccupyAsync(Guid id, OccupyCouponInput input)
        {
            return _service.OccupyAsync(id, input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CouponDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CouponDto>> GetListAsync(GetCouponListInput input)
        {
            return _service.GetListAsync(input);
        }
    }
}