using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using AutoMapper;
using EasyAbp.EShop.Plugins.Coupons.Coupons;

namespace EasyAbp.EShop.Plugins.Coupons
{
    public class CouponsDomainAutoMapperProfile : Profile
    {
        public CouponsDomainAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Coupon, CouponData>(MemberList.Destination);
            
            CreateMap<CouponTemplate, CouponTemplateData>(MemberList.Destination);
            CreateMap<CouponTemplateScope, CouponTemplateScopeData>(MemberList.Destination);
        }
    }
}
