using System;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public interface ICouponTemplateAppService :
        ICrudAppService< 
            CouponTemplateDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateCouponTemplateDto,
            CreateUpdateCouponTemplateDto>
    {

    }
}