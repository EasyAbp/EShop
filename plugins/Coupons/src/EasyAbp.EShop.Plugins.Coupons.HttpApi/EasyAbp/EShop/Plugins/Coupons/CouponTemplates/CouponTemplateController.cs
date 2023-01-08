using System;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    [RemoteService(Name = EShopPluginsCouponsRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/e-shop/plugins/coupons/coupon-template")]
    public class CouponTemplateController : CouponsController, ICouponTemplateAppService
    {
        private readonly ICouponTemplateAppService _service;

        public CouponTemplateController(ICouponTemplateAppService service)
        {
            _service = service;
        }

        [HttpPost]
        public virtual Task<CouponTemplateDto> CreateAsync(CreateUpdateCouponTemplateDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CouponTemplateDto> UpdateAsync(Guid id, CreateUpdateCouponTemplateDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CouponTemplateDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CouponTemplateDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}