using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using AutoMapper;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;

namespace EasyAbp.EShop.Plugins.Coupons
{
    public class CouponsHttpApiClientAutoMapperProfile : Profile
    {
        public CouponsHttpApiClientAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<CouponDto, CouponData>(MemberList.Destination);
            
            CreateMap<CouponTemplateDto, CouponTemplateData>(MemberList.Destination);
            CreateMap<CouponTemplateScopeDto, CouponTemplateScopeData>(MemberList.Destination);
        }
    }
}
