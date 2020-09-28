using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using AutoMapper;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EShop.Plugins.Coupons
{
    public class CouponsApplicationAutoMapperProfile : Profile
    {
        public CouponsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<CouponTemplate, CouponTemplateDto>();
            CreateMap<CreateUpdateCouponTemplateDto, CouponTemplate>(MemberList.Source)
                .Ignore(x => x.Scopes);
            
            CreateMap<CouponTemplateScope, CouponTemplateScopeDto>();
            CreateMap<CreateUpdateCouponTemplateScopeDto, CouponTemplateScope>(MemberList.Source);
            
            CreateMap<Coupon, CouponDto>()
                .Ignore(x => x.CouponTemplate);
            CreateMap<CreateCouponDto, Coupon>(MemberList.Source);
            CreateMap<UpdateCouponDto, Coupon>(MemberList.Source);
        }
    }
}
